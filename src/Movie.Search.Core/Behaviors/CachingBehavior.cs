using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Movie.Search.Core.Abstractions.Caching;

namespace Movie.Search.Core.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
        private readonly IEasyCachingProvider _cachingProvider;
        private readonly int defaultCacheExpirationInHours = 10;

        public CachingBehavior(IEasyCachingProviderFactory cachingFactory,
            ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _cachingProvider = cachingFactory.GetCachingProvider("mem");

        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (request is not ICacheRequest)
            {
                // Continue to next middleware
                return await next();
            }
            if (request is ICacheRequest cacheRequest)
            {
                var cacheKey = cacheRequest.CacheKey;
                var cachedResponse = await _cachingProvider.GetAsync<TResponse>(cacheKey);
                if (cachedResponse.Value != null)
                {
                    _logger.LogDebug("Fetch data from cache with cachKey: {CacheKey}", cacheKey);
                    return cachedResponse.Value;
                }

                var response = await next();

                var expirationTime = DateTime.Now.AddSeconds(defaultCacheExpirationInHours);

                await _cachingProvider.SetAsync(cacheKey, response, expirationTime.TimeOfDay);

                _logger.LogDebug("Set data to cahche with  cachKey: {CacheKey}", cacheKey);

                return response;
            }
            return await next();

        }
    }
}

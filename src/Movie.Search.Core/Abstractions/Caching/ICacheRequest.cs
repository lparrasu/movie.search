
namespace Movie.Search.Core.Abstractions.Caching
{
    public interface ICacheRequest
    {
        string CacheKey { get; }
        DateTime? AbsoluteExpirationRelativeToNow { get; }
    }

}

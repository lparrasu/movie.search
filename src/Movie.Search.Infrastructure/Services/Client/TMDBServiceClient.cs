using AutoMapper;
using Microsoft.Extensions.Options;
using Movie.Search.Core.Abstractions.ServiceClient;
using Movie.Search.Core.DTO.Movies;
using Movie.Search.Core.Entities.ConfigOptions;
using Movie.Search.Core.Entities.General;
using TMDbLib.Client;

namespace Movie.Search.Infrastructure.Services.Client
{

    public class TMDBServiceClient : ITMDBServiceClient
    {
        private readonly IMapper _mapper;
        private readonly TMDbClient _client;
        private readonly TMDBOptions _tmdbOptions;


        public TMDBServiceClient(IOptions<TMDBOptions> options,IMapper mapper )
        {
            _mapper = mapper;
            _tmdbOptions = options.Value;
            _client = new TMDbClient(_tmdbOptions.ApiKey);

        }

        public async Task<ListContainer<MovieDto>> SearchMovieAsync(string keyword, int page = 1, string language = "en-US", 
            string region = "US", bool includeAdult = false, int year = 0,
            int primaryReleaseYear = 0, CancellationToken cancellationToken = default)
        {

            var searchResult = await _client.SearchMovieAsync(keyword,language, page,
                includeAdult, year,
                region, primaryReleaseYear, cancellationToken);

            return _mapper.Map<ListContainer<MovieDto>>(searchResult);

           
        }
    }
}

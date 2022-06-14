using MediatR;
using Movie.Search.Core.Abstractions.ServiceClient;

namespace Movie.Search.Core.Features.Movies
{
    public class SearchMovieQueryHandler : IRequestHandler<SearchMovieQuery, SearchMovieResult>
    {
        private readonly ITMDBServiceClient _movieDbServiceClient;
        //private readonly IMapper _mapper;

        public SearchMovieQueryHandler(ITMDBServiceClient movieDbServiceClient)
        {
            _movieDbServiceClient = movieDbServiceClient;
         
        }

        public async Task<SearchMovieResult> Handle(SearchMovieQuery query, CancellationToken cancellationToken)
        {
            
            var movies = await _movieDbServiceClient.SearchMovieAsync(keyword: query.SearchKeywords,
                page: query.Page,query.Language, region: query.Region, includeAdult: query.IncludeAdult, year: query.Year, primaryReleaseYear: query
                    .PrimaryReleaseYear, cancellationToken: cancellationToken);


            return new SearchMovieResult { Movies = movies };
        }
    }
}

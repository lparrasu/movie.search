using MediatR;
using Movie.Search.Core.Abstractions.Caching;

namespace Movie.Search.Core.Features.Movies
{
    public class SearchMovieQuery : IRequest<SearchMovieResult>, ICacheRequest
    {
        public SearchMovieQuery(string searchKeywords, int page = 1, string language = "en-US", string region = "US", int year = 0, int primaryReleaseYear = 0,
           bool includeAdult = false)
        {
            SearchKeywords = searchKeywords;
            Page = page;
            Language = language;
            Region = region;
            Year = year;
            PrimaryReleaseYear = primaryReleaseYear;
            IncludeAdult = includeAdult;
        }

        public string SearchKeywords { get; }
        public string Language { get; set; } = "en-US";
        public string Region { get; }
        public int Year { get; }
        public int PrimaryReleaseYear { get; }
        public bool IncludeAdult { get; }
        public int Page { get; }

        public string CacheKey => $"SearchKeywords_{SearchKeywords?.ToLower().Trim()}_Page_{Page}_Language_{Language.ToLower().Trim()}_Region_{Region.ToLower().Trim()}_IncludeAdult_{IncludeAdult.ToString()}_PrimaryReleaseYear_{PrimaryReleaseYear}_Year_{Year}";

        public DateTime? AbsoluteExpirationRelativeToNow => DateTime.Now.AddSeconds(60);

       
        

    }
}

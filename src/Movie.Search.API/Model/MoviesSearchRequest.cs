namespace Movie.Search.API.Model
{
    public class MoviesSearchRequest
    {
        public string SearchKeywords { get; set; }
        public int Page { get; set; } = 1;
        public string Language { get; set; } = "en-US";
        public string Region { get; set; } = "US";
        public int Year { get; set; }
        public int PrimaryReleaseYear { get; set; }
        public bool IncludeAdult { get; set; }
    }
}

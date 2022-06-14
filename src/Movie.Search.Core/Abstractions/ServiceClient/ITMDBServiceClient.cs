using Movie.Search.Core.DTO.Movies;
using Movie.Search.Core.Entities.General;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Movie.Search.Core.Abstractions.ServiceClient
{
    public interface ITMDBServiceClient
    {
        Task<ListContainer<MovieDto>> SearchMovieAsync(string keyword,
          int page = 1,
          string language = "en-US",
          string region = "US",
          bool includeAdult = false,
          int year = 0,
          int primaryReleaseYear = 0,
          CancellationToken cancellationToken = default);
    }
}

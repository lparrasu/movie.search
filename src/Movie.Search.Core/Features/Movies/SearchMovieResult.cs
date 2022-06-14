using Movie.Search.Core.DTO.Movies;
using Movie.Search.Core.Entities.General;

namespace Movie.Search.Core.Features.Movies
{
    public class SearchMovieResult
    {
        public ListContainer<MovieDto> Movies { get; set; }
    }
}

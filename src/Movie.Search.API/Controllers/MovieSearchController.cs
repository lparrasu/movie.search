using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie.Search.API.Model;
using Movie.Search.Core.Features.Movies;

namespace Movie.Search.API.Controllers
{
    [Route("api/searchmovie")]
    [ApiController]
    public class MovieSearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public async Task<ActionResult> SearchAsync([FromQuery] MoviesSearchRequest request,
            CancellationToken cancellationToken)
        {
            var query = new SearchMovieQuery(request.SearchKeywords, request.Page,request.Language,  request.Region,
                request.Year, request.PrimaryReleaseYear, request.IncludeAdult);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}

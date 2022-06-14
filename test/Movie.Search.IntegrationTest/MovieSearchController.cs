using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Movie.Search.API.Model;
using Movie.Search.Core.DTO.Movies;
using Movie.Search.Core.Entities.General;
using Movie.Search.Core.Features.Movies;
using System.Net;
using System.Net.Http.Json;
using System.Web;

namespace Movie.Search.IntegrationTests
{
    public class MovieSearchController : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MovieSearchController(WebApplicationFactory<Program> application)
        {
            _client = application.CreateClient();
        }

        private Task<HttpResponseMessage> Act(MoviesSearchRequest request)
        {
            var queryString = ToQueryString(request); 
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"/api/searchmovie/search?{queryString}",
                    UriKind.RelativeOrAbsolute)
               
            };
            return _client.SendAsync(httpRequestMessage);
        }

        [Fact]
        public async Task search_movie_endpoint_should_return_http_status_code_ok()
        {
            // Act
            var response =
                await Act(new MoviesSearchRequest { Page = 1, SearchKeywords = "Endgame", Language = "es-es", Region="CO" });

            // Assert
            response.IsSuccessStatusCode.Should().Be(true);
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task search_movie_endpoint_should_return_correctData()
        {
            // Act
            var response =
                await Act(new MoviesSearchRequest { Page = 1, SearchKeywords = "Avengers", Language = "es-es", Region = "CO" });
            var result = await response.Content.ReadFromJsonAsync<SearchMovieResult>();

            // Assert
            response.IsSuccessStatusCode.Should().Be(true);
            result.Should().NotBeNull();
            result.Should().BeOfType<SearchMovieResult>();
            result?.Movies.Should().NotBeNull();
            result?.Movies.Should().BeOfType<ListContainer<MovieDto>>();
            result?.Movies.Results.Should().NotBeNull();
            result?.Movies.Results.Any().Should().BeTrue();
            result?.Movies.Page.Should().Be(1);
            
        }

        private string ToQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
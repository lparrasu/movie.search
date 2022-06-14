using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Search.Infrastructure.Services.Client
{
    //https://developers.google.com/youtube/v3/code_samples/dotnet
    internal class YoutubeServiceClient
    {
        public async Task<> GetTrailers(string movieName,
            int pageSize = 20)
        {
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = _options.ApiKey,
                ApplicationName = GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List(_options.SearchPart);

            var searchTerm = movieName.ToLower().Contains("trailer") == false
                ? $"{movieName.ToLower()} trailer"
                : movieName.ToLower();

            searchListRequest.Q = searchTerm;
            //searchListRequest.Order =
            searchListRequest.MaxResults = pageSize;
            searchListRequest.PageToken = pageToken;
            searchListRequest.Type = _options.SearchType;
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True__;


            var searchListResponse = await _retryPolicy.ExecuteAsync(() => searchListRequest.ExecuteAsync());

            var result = new VideoListResultModel<MovieSearch.Core.Generals.Video>(items: searchListResponse.Items
                    .Select(x =>
                        new MovieSearch.Core.Generals.Video
                        {
                            Iso_639_1 = "en",
                            Iso_3166_1 = "US",
                            Id = x.Id.VideoId,
                            Name = x.Snippet.Title,
                            Size = 1080,
                            Site = "YouTube",
                            Key = x.Id.VideoId,
                            PublishedAt = x.Snippet.PublishedAt,
                            Type = "Trailer"
                        }).ToList(),
                totalItems: searchListResponse.PageInfo.TotalResults ?? 0, pageToken: pageToken,
                nextPageToken: searchListResponse.NextPageToken,
                previousPageToken: searchListResponse.PrevPageToken,
                pageSize: searchListResponse.PageInfo.ResultsPerPage ?? 0);

            return result;
        }
    }
}

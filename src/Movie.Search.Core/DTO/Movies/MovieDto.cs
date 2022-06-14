using Movie.Search.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Search.Core.DTO.Movies
{
    public class MovieDto
    {
        public MediaType MediaType { get; init; } = MediaType.Movie;

        public bool Adult { get; set; }

        public string OriginalTitle { get; set; }


        public DateTime? ReleaseDate { get; set; }


        public string Title { get; set; }


        public bool Video { get; set; }

        public string BackdropPath { get; set; }

        public List<int> GenreIds { get; set; }

        public string OriginalLanguage { get; set; }
        public string Overview { get; set; }

        public string PosterPath { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }
    }
}

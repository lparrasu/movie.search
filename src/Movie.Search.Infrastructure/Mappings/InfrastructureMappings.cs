using AutoMapper;
using Movie.Search.Core.DTO.Movies;
using Movie.Search.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace Movie.Search.Infrastructure.Mappings
{
    public class InfrastructureMappings : Profile
    {
        public InfrastructureMappings()
        {
            CreateMap<SearchMovie, MovieDto>();
            CreateMap(typeof(SearchContainer<>), typeof(ListContainer<>));



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Search.Core.Entities.General
{
    public class ListContainer<T>
    {

        public int Page { get; set; }


        public List<T> Results { get; set; }


        public int TotalPages { get; set; }


        public int TotalResults { get; set; }
    }
}

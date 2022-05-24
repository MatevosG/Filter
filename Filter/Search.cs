using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public class Search
    {
        public Search()
        {
            searchModels = new List<SearchModel>();
        }
        public List<SearchModel> searchModels { get; set; }
    }
}

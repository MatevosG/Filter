using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public class SearchModel
    {
        public string PropertyName { get; set; }
        public string SearchQuery { get; set; }
        public SearchTypes Typ { get; set; }
    }
}

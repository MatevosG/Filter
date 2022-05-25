using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public class Program
    {
        static void Main(string[] args)
        {
            AuthorContext authorService = new AuthorContext();
            SearchModel searchModel = new SearchModel();
            searchModel.SearchQuery = "50";
            searchModel.PropertyName = "Age";
            searchModel.Typ = SearchTypes.LessOrEqual;

            SearchModel searchModel1 = new SearchModel();
            searchModel1.PropertyName = "FirstName";
            searchModel1.SearchQuery = "test";
            searchModel1.Typ = SearchTypes.Equals;

            Search search = new Search();
            search.searchModels.Add(searchModel);
            search.searchModels.Add(searchModel1);
            
            var filterValues = authorService.authorList.AsQueryable().Filter(search);
        }
    }
}

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
            AuthorService authorService = new AuthorService();
            SearchModel searchModel = new SearchModel();
            searchModel.SearchQuery = "50";
            searchModel.PropertyName = "Age";
            searchModel.Typ = SearchTypes.Gret;
            SearchModel searchModel1 = new SearchModel();
            searchModel1.PropertyName = "FirstName";
            searchModel1.SearchQuery = "hovo";
            searchModel1.Typ = SearchTypes.Equals;
            Search search = new Search();
            search.searchModels.Add(searchModel);
            search.searchModels.Add(searchModel1);
            //var kkk = authorService.authorList.AsQueryable().FilterQuery(search);
            var vvvvvv = authorService.authorList.AsQueryable().TryFilter(search);


            var ses = authorService.Get(searchModel);

        }
    }

    public static class Test
    {
        public static IQueryable<T> TryFilter<T>(this IQueryable<T> source, Search search)
        {
            List<T> start = new List<T>();
            if (search.searchModels.Count > 1)
            {
                List<T> starttemp = new List<T>();
                List<T> temp = new List<T>();
                for (int i = 0; i < search.searchModels.Count; i++)
                {
                    var res = source.FilterQuery(search.searchModels[i]);
                    if (i == 0)
                    {

                    }
                }
                return start.AsQueryable();
            }

            //query.AsQueryable().FilterQuery(search);
            return start.AsQueryable();
        }
        public static IQueryable<T> FilterQuery<T>(this IQueryable<T> source, SearchModel search)
        {
            var filterCollection = new List<T>();
            var mytype = typeof(T);
            
                foreach (var item in source)
                {
                    var propertyInfo =
                       item.GetType()
                           .GetProperty(search.PropertyName, BindingFlags.Public | BindingFlags.Instance);

                    var value = propertyInfo.GetValue(item, null);

                    if (search.Typ == SearchTypes.Equals)
                    {
                        var searchValue = search.SearchQuery.Trim();
                        if (string.Equals(value, searchValue))
                            filterCollection.Add(item);
                    }

                    if (search.Typ == SearchTypes.Contains)
                    {
                        if (value.ToString().Contains(search.SearchQuery))
                            filterCollection.Add(item);
                    }

                    if (search.Typ == SearchTypes.Gret)
                    {
                        var searchValue = int.Parse(search.SearchQuery);
                        var valueGreat = int.Parse(value.ToString());
                        if (valueGreat > searchValue)
                            filterCollection.Add(item);
                    }
                    if (search.Typ == SearchTypes.Less)
                    {
                        var searchValue = int.Parse(search.SearchQuery);
                        var valueGreat = int.Parse(value.ToString());
                        if (valueGreat < searchValue)
                            filterCollection.Add(item);
                    }
                    if (search.Typ == SearchTypes.GreaterOrEqual)
                    {
                        var searchValue = int.Parse(search.SearchQuery);
                        var valueGreat = int.Parse(value.ToString());
                        if (valueGreat >= searchValue)
                            filterCollection.Add(item);
                    }
                    if (search.Typ == SearchTypes.LessOrEqual)
                    {
                        var searchValue = int.Parse(search.SearchQuery);
                        var valueGreat = int.Parse(value.ToString());
                        if (valueGreat <= searchValue)
                            filterCollection.Add(item);
                    }
                }
            

            return filterCollection.AsQueryable();
        }
    }

    public class AuthorService
    {
        public List<Author> authorList = new List<Author>()
        {
            new Author(){Id=1,FirstName="hovo",LastName="tumanyan",Age=150,Category="patmvacq" },
            new Author(){Id=2,FirstName="avo",LastName="isahakyan",Age=130,Category="banastexcutun" },
            new Author(){Id=3,FirstName="hovo",LastName="yesim",Age=50,Category="epos" },
            new Author(){Id=4,FirstName="vardan",LastName="sedrakyan",Age=60,Category="epos" },
            new Author(){Id=5,FirstName="test",LastName="testyan",Age=50,Category="patmvacq" },
            new Author(){Id=7,FirstName="ttttt",LastName="tttttt",Age=27,Category="tttttt" },
            new Author(){Id=8,FirstName="kkkkk",LastName="kkkkkk",Age=25,Category="kkkkk" },
            new Author(){Id=9,FirstName="vvvvv",LastName="vvvvvv",Age=45,Category="vvvvvv" },
        };

        public List<Author> Get(SearchModel searchModel)
        {
            var ppp = authorList.AsQueryable().TextFilter(searchModel.SearchQuery).ToList();
            return ppp;
        }
    }


}

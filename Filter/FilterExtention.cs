using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public static class FilterExtention
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, Search search)
        {

            if (search.searchModels.Count > 1)
            {
                List<T> Filteres = new List<T>();
                var caunt = search.searchModels.Count + 1;

                foreach (var item in search.searchModels)
                {
                    caunt--;
                    if (caunt == 1)
                    {
                        return Filteres.AsQueryable().FilterQuery(item);
                    }
                    var res = source.FilterQuery(item);
                    foreach (var itemres in res)
                    {
                        Filteres.Add(itemres);
                    }
                }
                return Filteres.AsQueryable();
            }
            return source.FilterQuery(search.searchModels[0]);
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
}

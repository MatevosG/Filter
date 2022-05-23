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
            var res = authorService.Get(new AuthorSearchModel() {SearchQueryInt=0,SearchQuery= "patmutyun" });
            //var ttt = authorService.authors.AsQueryable().TextFilter("patmutyun");
            

        }
        public List<T> Filter<T>(List<T> collection, string property, string filterValue)
        {
            var filteredCollection = new List<T>();
            foreach (var item in collection)
            {
                var propertyInfo =
                    item.GetType()
                        .GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    throw new NotSupportedException("property given does not exists");

                var propertyValue = propertyInfo.GetValue(item, null);
                if (propertyValue == filterValue)
                    filteredCollection.Add(item);
            }

            return filteredCollection;
        }
    }
   
    public static class FilterExtention
    {
        public static IQueryable<T> TextFilter<T>(this IQueryable<T> source, string term)
        {
            if (string.IsNullOrEmpty(term)) { return source; }

            Type elementType = typeof(T);

            PropertyInfo[] stringProperties =
                elementType.GetProperties()
                    .Where(x => x.PropertyType == typeof(string))
                    .ToArray();
            if (!stringProperties.Any()) { return source; }

            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            ParameterExpression prm = Expression.Parameter(elementType);

            IEnumerable<Expression> expressions = stringProperties
                .Select(prp =>
                   Expression.Call(
                      Expression.Property(
                            prm,
                            prp
                        ),
                        containsMethod,
                      Expression.Constant(term)
                    )
                );

            Expression body = expressions
                .Aggregate(
                    (prev, current) => prev==null?current:prev
                );

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(body, prm);

            return source.Where(lambda);
        }
    }
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Genre { get; set; }
    }
    public class AuthorService
    {
        public List<Author> authors = new List<Author>()
            {
                new Author(){Id=1,Name="test",Surname="tetyan",Age=50,Genre = "arkacayin" },
                new Author(){Id=2,Name="hov",Surname="tuman",Age=150,Genre = "patmutyun" },
                new Author(){Id=3,Name="test",Surname="azizyan",Age=50,Genre = "epos" },
                new Author(){Id=4,Name="vardan",Surname="sedrakyan",Age=50,Genre = "patmutyun" },
                new Author(){Id=5,Name="avetiq",Surname="isahakyan",Age=50,Genre = "arkacayin" },
            };
        public List<Author> Get(AuthorSearchModel authorSearch)
        {
            var author = authors.AsQueryable().TextFilter(authorSearch.SearchQuery); 
            return author.ToList();
        }
    }
    public class AuthorSearchModel
    {
        public string SearchQuery { get; set; }
        public int SearchQueryInt { get; set; }
    }
}

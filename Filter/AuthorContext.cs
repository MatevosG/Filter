using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public class AuthorContext
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
    }
}

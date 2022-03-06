using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorException
{
    public class InvalidWordLength:Exception
    {
        public string errorMassage;
        public InvalidWordLength(int expectedWordLength)
        {
            errorMassage = $"Word's length should be {expectedWordLength}";
        }
    }
}

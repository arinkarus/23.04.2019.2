using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    internal class Book
    {
        public int Pages { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public override string ToString()
        {
            return Name.ToString();          
        }
    }
}

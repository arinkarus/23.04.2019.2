using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    public class IntComparerByStringLength : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.ToString().Length - y.ToString().Length;
        }
    }
}

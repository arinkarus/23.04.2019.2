using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    public class StringAscByOccurrencesComparer : IComparer<string>
    {
        private readonly char symbol;

        public StringAscByOccurrencesComparer(char symbol)
        {
            this.symbol = symbol;
        }

        public int Compare(string x, string y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }


            int occurrencesForX = GetCountOfSymbol(x, this.symbol);
            int occurrencesForY = GetCountOfSymbol(y, this.symbol);
            return occurrencesForX - occurrencesForY;
        }

        private int GetCountOfSymbol(string text, char givenSymbol)
        {
            return text.Split(givenSymbol).Length - 1;
        }
    }
}
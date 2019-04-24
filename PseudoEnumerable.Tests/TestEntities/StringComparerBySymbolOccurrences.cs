using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable.Tests
{
    public class StringAscByOccurrencesComparer : IComparer<string>
    {
        /// <summary>
        /// Symbol for counting occurrences.
        /// </summary>
        private readonly char symbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringAscByOccurrencesComparer"/> class.
        /// </summary>
        /// <param name="symbol">Symbol for counting occurrences.</param>
        public StringAscByOccurrencesComparer(char symbol)
        {
            this.symbol = symbol;
        }

        /// <summary>
        /// Compares two strings depending on the occurrences of given symbols in strings.
        /// </summary>
        /// <param name="x">First string.</param>
        /// <param name="y">Second string.</param>
        /// <returns> Returns an integer that indicates their relative position in the sort order.</returns>
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
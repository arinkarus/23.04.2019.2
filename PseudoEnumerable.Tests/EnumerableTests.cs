using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
      
        [TestCase(new int[] { 12, 7, 10, 0, 3, -8, -9 }, ExpectedResult = new int[] { 12, 10, 0, -8 })]
        public int[] Filter_IsEven_ReturnFilteredArray(int[] array)
        {
            return array.Filter(x => x % 2 == 0).ToArray();
        }

        [Test]
        public void Filter_StringLengthLessOrEqual3_ReturnFilteredArray()
        {
            var given = new string[] { "one", "two", "three", "o"};
            var expected = new string[] { "one", "two", "o" };
            var actual = given.Filter(x => x.Length <= 3).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        public class Book 
        {
            public int Pages { get; set; }

            public string Name { get; set; }

            public string Author { get; set; }
        }

        [Test]
        public void Filter_BookPagesGreaterThan50_ReturnFilteredBooks()
        {
            int[] pages = new int[5];
            var books = new Book[]
            {
                new Book { Name = "Name1", Pages = 80 },
                new Book { Name = "Name2", Pages  = 40 },
                new Book { Name = "Name3", Pages = 100 },
                new Book { Name = "Name4", Pages = 20 },
                new Book { Name = "Name4", Pages = 60 }
            };
            var actual = books.Filter(x => x.Pages > 50).ToArray();
            var expected = new Book[]
            {
                new Book { Name = "Name1", Pages = 80 },
                new Book { Name = "Name3", Pages = 100 },
                new Book { Name = "Name4", Pages = 60 }
            };
            var expectedPages = new int[3] { 80, 100, 60 };
            var actualPages = new int[actual.Length];
            int i = 0;
            foreach(var item in actual)
            {
                actualPages[i++] = item.Pages;
            }
            CollectionAssert.AreEqual(expectedPages, actualPages);
        }

        [TestCase(new int[] { 12, 10, 0, 4, -8, 88 }, ExpectedResult = true)]
        [TestCase(new int[] { 12, 7, 10, 0, 3, -8, -9 }, ExpectedResult = false)]
        public bool ForAll_IsEven_ReturnFilteredArray(int[] array)
        {
            return array.ForAll(x => x % 2 == 0);
        }
    }
}
using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoLINQ;

namespace PseudoEnumerable.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Filter tests

        [TestCase(new int[] { 12, 7, 10, 0, 3, -8, -9 }, ExpectedResult = new int[] { 12, 10, 0, -8 })]
        public IEnumerable<int> Filter_IsEven_ReturnFilteredArray(int[] array)
        {
            return array.Filter(x => x % 2 == 0);
        }

        [Test]
        public void Filter_StringLengthLessOrEqual3_ReturnFilteredList()
        {
            List<string> given = new List<string>() { "one", "two", "three", "o" };
            List<string> expected = new List<string>() { "one", "two", "o" };
            var actual = given.Filter(x => x.Length <= 3);
            var actualList = new List<string>(actual);
            CollectionAssert.AreEqual(expected, actualList);
        }

        [Test]
        public void Filter_ObjectsNotNull_ReturnFilteredObjects()
        {
            var book = new Book() { Name = "Name", Pages = 120 };
            var givenList = new List<object>() { 5, 4, null, "hello", null, book, null, null };
            var expectedList = new object[] { 5, 4, "hello", book };
            var actualList = new List<object>(givenList.Filter(x => x != null));
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [Test]
        public void Filter_BookPagesGreaterThan50_ReturnFilteredBooks()
        {
            int[] pages = new int[5];
            var first = new Book { Name = "Name1", Pages = 80 };
            var second = new Book { Name = "Name2", Pages = 40 };
            var third = new Book { Name = "Name3", Pages = 100 };
            var forth = new Book { Name = "Name4", Pages = 20 };
            var fifth = new Book { Name = "Name5", Pages = 60 };
            var books = new List<Book> { first, second, third, forth, fifth };
            var actual = new List<Book>(books.Filter(x => x.Pages > 50));
            var expected = new Book[]
            {
                first,
                third,
                fifth
            };
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Filter_PointsIsXPosititveOrZero_ReturnFilteredPoints()
        {
            var first = new Point() { X = -5, Y = 10 };
            var second = new Point() { X = 0, Y = 4 };
            var third = new Point() { X = 85, Y = -10 };
            var forth = new Point() { X = -10, Y = -10 };
            var fifth = new Point() { X = -5, Y = 10 };
            var points = new List<Point>() { first, second, third, forth, fifth };
            var expectedList = new List<Point>() { second, third };
            var actualList = new List<Point>(points.Filter(x => x.X >= 0));
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [Test]
        public void Filter_PredicateIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter<object>(new List<object> { 4 }, null));

        [Test]
        public void Filter_SourceIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.Filter<object>(null, x => x != null));

        #endregion

        #region Transform tests

        [Test]
        public void Transform_TransformerIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform<object, object>(new List<object> { 4 }, null));

        [Test]
        public void Transform_SourceIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.Transform<object, object>(null, x => x != null));


        [TestCase(new int[] { 12, 7, 10, 0, 3, -8, -9 }, ExpectedResult = new int[] { 14, 9, 12, 2, 5, -6, -7 })]
        public IEnumerable<int> Transform_Plus2_ReturnTransformedValues(int[] array)
        {
            return array.Transform(x => x + 2);
        }

        [TestCase(new int[] { 12, 7, 10, 8 }, ExpectedResult = new string[] { "12", "7", "10", "8" })]
        public IEnumerable<string> Transform_IntToString_ReturnTransformedValues(int[] array)
        {
            return array.Transform(x => x.ToString());
        }

        [Test]
        public void Transform_GetNameOfBooks_ReturnTransformedValues()
        {
            int[] pages = new int[5];
            var first = new Book { Name = "Name1", Pages = 80 };
            var second = new Book { Name = "Name2", Pages = 40 };
            var third = new Book { Name = "Name3", Pages = 100 };
            var forth = new Book { Name = "Name4", Pages = 20 };
            var fifth = new Book { Name = "Name5", Pages = 60 };
            var books = new List<Book> { first, second, third, forth, fifth };
            var expected = new List<string>() { "Name1", "Name2", "Name3", "Name4", "Name5" };
            var actual = new List<string>(books.Transform<object, string>(x => x.ToString()));
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Transform_GetAbsPoints_ReturnTransormedValues()
        {
            List<Point> points = new List<Point> { new Point { X = 5, Y = -10 }, new Point { X = 10, Y = 10 },
                new Point { X = -88, Y = -1}
            };
            var expected = new List<Point> { new Point { X = 5, Y = 10 }, new Point { X = 10, Y = 10 }, new Point { X = 88, Y = 1 } };
            List<Point> actualPoints = new List<Point>(points.Transform(x => new Point { X = Math.Abs(x.X), Y = Math.Abs(x.Y) }));
            CollectionAssert.AreEqual(expected, actualPoints);
        }

        [Test(ExpectedResult = new int[] { 10, 20, -4 })]
        public IEnumerable<int> Transform_GetXCoordinateMultipliedBy2_ReturnTransformedValues()
        {
            List<Point> points = new List<Point> { new Point { X = 5, Y = -10 }, new Point { X = 10, Y = 10 },
                new Point { X = -2, Y = -1}
            };
            return points.Transform(x => x.X * 2);
        }

        #endregion

        #region Sort Tests

        [TestCase(new int[] { 10, 5, 8, 7, 11, 20, 5 }, ExpectedResult = new int[] { 5, 5, 7, 8, 10, 11, 20 })]
        public IEnumerable<int> SortBy_Int_ReturnSortedValues(int[] given)
        {
            return given.SortBy(x => x);
        }

        [TestCase(new int[] { 100, 555, 10000, 1 }, ExpectedResult = new int[] { 1, 100, 555, 10000 })]
        public IEnumerable<int> SortBy_IntByStringLength_ReturnSortedValues(int[] given)
        {
            return given.SortBy(x => x.ToString().Length);
        }

        [TestCase(new int[] { 10, 5, 8, 7, 11, 20, 5 }, ExpectedResult = new int[] { 20, 11, 10, 8, 7, 5, 5 })]
        public IEnumerable<int> SortBy_IntReverseKey_ReturnSortedValues(int[] given)
        {
            return given.SortBy(x => -x);
        }

        [TestCase(new int[] { 100, 555, 10000, 1 }, ExpectedResult = new int[] { 1, 100, 555, 10000 })]
        public IEnumerable<int> SortBy_IntByStringLengthWithComparer_ReturnSortedValues(int[] given)
        {
            return given.SortBy(x => x, new IntComparerByStringLength());
        }

        [TestCase(new int[] { 100, 555, 1000, 1 }, ExpectedResult = new int[] { 1000, 100, 555, 1 })]
        public IEnumerable<int> SortByDescending_IntByStringLengthDesc_ReturnSortedValues(int[] given)
        {
            return given.SortByDescending(x => x.ToString().Length);
        }

        [TestCase(new int[] { 10, 5, 8, 7, 11, 20, 5 }, ExpectedResult = new int[] { 20, 11, 10, 8, 7, 5, 5 })]
        public IEnumerable<int> SortByDescending_Int_ReturnSortedValues(int[] given)
        {
            return given.SortByDescending(x => x);
        }

        [TestCase(new int[] { 1, 11, 777, 1001010, 44444 }, ExpectedResult = new int[] { 1001010, 44444, 777, 11, 1 })]
        public IEnumerable<int> SortByDescending_IntByStringLengthDescWithComparer_ReturnSortedValues(int[] given)
        {
            return given.SortByDescending(x => x, new IntComparerByStringLength());
        }

        [Test]
        public void SortBy_SourceIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy<int, int>(null, x => x));

        [Test]
        public void SortBy_KeyIsEmpty_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Enumerable.SortBy<int, int>(new int[] { 5, 4 }, null));

        [Test]
        public void SortBy_DefaultComparerIsNotFound_ThrowComparerNotFoundException() =>
             Assert.Throws<ComparerNotFound>(() => Enumerable.SortBy<Book, Book>(new Book[] { }, x => x));

        [Test]
        public void SortByDescending_BooksByNameDesc_ReturnSortedBooks()
        {
            var first = new Book { Name = "Alex", Pages = 101 };
            var second = new Book { Name = "Daniel", Pages = 50 };
            var third = new Book { Name = "Bob", Pages = 1015 };
            var bookList = new List<Book> { first, second, third };
            var expectedList = new List<Book> { second, third, first };
            var actual = bookList.SortByDescending(x => x.Name);
            var actualList = new List<Book>(actual);
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestCase(arg1: new string[] { "hello", "link to", "one", "two", "hello hello" }, arg2: 'l', ExpectedResult =
            new string[] { "one", "two", "link to", "hello", "hello hello" })]
        public IEnumerable<string> SortBy_StringByOccuranciesOfSymbol_ReturnSorted(string[] given, char symbol)
        {
            return given.SortBy(x => x, new StringAscByOccurrencesComparer(symbol));
        }

        [TestCase(arg1: new string[] { "hello", "link to", "one", "two", "hello hello" }, arg2: 'l', ExpectedResult =
             new string[] { "hello hello", "hello", "link to", "one", "two" })]
        public IEnumerable<string> SortByDescing_StringByOccuranciesOfSymbol_ReturnSorted(string[] given, char symbol)
        {
            var result = given.SortByDescending(x => x, new StringAscByOccurrencesComparer(symbol));
            List<string> str = new List<string>(result);
            return result;
        }

        #endregion

        #region ForAll tests

        [TestCase(new int[] { 12, 10, 0, 4, -8, 88 }, ExpectedResult = true)]
        [TestCase(new int[] { 12, 7, 10, 0, 3, -8, -9 }, ExpectedResult = false)]
        public bool ForAll_IsEven_ReturnFilteredArray(int[] array)
        {
            return array.ForAll(x => x % 2 == 0);
        }

        [Test]
        public void ForAll_BooksPagesGreaterThan80_ReturnFalse()
        {
            int[] pages = new int[5];
            var first = new Book { Name = "Name1", Pages = 80 };
            var second = new Book { Name = "Name2", Pages = 40 };
            var third = new Book { Name = "Name3", Pages = 100 };
            var forth = new Book { Name = "Name4", Pages = 20 };
            var fifth = new Book { Name = "Name5", Pages = 60 };
            var books = new List<Book> { first, second, third, forth, fifth };
            var expected = new List<string>() { "Name1", "Name2", "Name3", "Name4", "Name5" };
            Assert.IsFalse(books.ForAll(x => x.Pages > 80));
        }

        [TestCase(arg: new string[] { "one", "two", "three" }, ExpectedResult = false)]
        public bool ForAll_StringLengthIs3_ReturnResult(string[] array)
        {
            return array.ForAll(x => x.Length == 3);
        }

        [TestCase(arg: new string[] { "", "hi" }, ExpectedResult = false)]
        [TestCase(arg: new string[] { "", null, "day" }, ExpectedResult = false)]
        [TestCase(arg: new string[] { "one", "two", "tree" }, ExpectedResult = true)]
        public bool ForAll_StringIsNotNullOrEmpty_ReturnResult(string[] array)
        {
            return array.ForAll(x => x != null && x != String.Empty);
        }

        #endregion

        #region CastTo tests

        [TestCase(arg: new object[] { 5, 10, 45 }, ExpectedResult = new int[] { 5, 10, 45 })]
        public IEnumerable<int> CastTo_Objects_ReturnInt(object[] objs)
        {
            return Enumerable.CastTo<int>(objs);
        }

        [TestCase(arg: new object[] { 12, "", 13 })]
        public void CastToTests_WithNotInt_ThrowInvalidCastException(IEnumerable<object> source)
        {
            var result = Enumerable.CastTo<int>(source);
            Assert.Throws<InvalidCastException>(() => { foreach (var item in result) { } });
        }

        #endregion

        #region Number generator tests

        [Test]
        public void GenerateNumbers_CountIsNotPositive_ThrowArgumentNullException() =>
          Assert.Throws<ArgumentNullException>(() => Enumerable.GenerateNumbers(110, 0));

        [TestCase(-1, 3, ExpectedResult = new int[] { -1, 0, 1 })]
        [TestCase(5, 5, ExpectedResult = new int[] { 5, 6, 7, 8, 9 })]
        public IEnumerable<int> GenerateNumbers_CountAndLowerBound_ReturnNumbers(int start, int count) =>
            Enumerable.GenerateNumbers(start, count);

        #endregion
    }
}
using NUnit.Framework;
using Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Abstract;
using Library.LibraryItems;
using Library.LibraryItemWriters;
using Library.Parsers;

namespace Library.Tests
{
    [TestFixture()]
    public class LibraryTests
    {
        private Library _library;

        [SetUp]
        public void Initialize()
        {
            _library = new Library();
            _library.AddParsers(new BookParser(), new NewsPaperParser(), new PatentParser());
            _library.AddWriters(new BookWriter(), new NewsPaperWriter(), new PatentWriter());
        }

        [Test()]
        public void BooksReadTest()
        {
            TextReader sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
                                             "<catalog>" +
                                             GetBookXml() +
                                             "</catalog>");
            IEnumerable<ILibraryItem> books = _library.ReadFrom(sr).ToList();

            CollectionAssert.AreEqual(books, new[]
            {
                GetBook()
            }, new BooksComparer());

            sr.Dispose();
        }

        [Test]
        public void PapersReadTest()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                             GetNewspaperXml() +
                                             "</catalog>");

            IEnumerable<ILibraryItem> newspapers = _library.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                GetNewsPaper()
            }, new NewsPaperComparer());

            sr.Dispose();
        }

        [Test]
        public void PatentsReadTest()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                             GetPatentXml() +
                                             "</catalog>");

            IEnumerable<ILibraryItem> newspapers = _library.ReadFrom(sr);

            CollectionAssert.AreEqual(newspapers, new[]
            {
                GetPatent()
            }, new PatentComparer());

            sr.Dispose();
        }

        [Test]
        public void AllLibraryItemsReadTest()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                             GetPatentXml() +
                                             GetBookXml() +
                                             GetNewspaperXml() +
                                             "</catalog>");

            List<ILibraryItem> entities = _library.ReadFrom(sr).ToList();

            Assert.IsTrue(new PatentComparer().Compare(entities[0], GetPatent()) == 0);
            Assert.IsTrue(new BooksComparer().Compare(entities[1], GetBook()) == 0);
            Assert.IsTrue(new NewsPaperComparer().Compare(entities[2], GetNewsPaper()) == 0);

            sr.Dispose();
        }

        [Test]
        public void AllLibraryItemsWriteTest()
        {
            StringBuilder actualResult = new StringBuilder();
            StringWriter sw = new StringWriter(actualResult);
            var book = GetBook();
            var newspaper = GetNewsPaper();
            var patent = GetPatent();
            var entities = new ILibraryItem[]
            {
                book,
                newspaper,
                patent
            };
            string expectedResult = @"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                    "<catalog>" +
                                    GetBookXml() +
                                    GetNewspaperXml() +
                                    GetPatentXml() +
                                    "</catalog>";

            _library.WriteTo(sw, entities);
            sw.Dispose();

            Assert.AreEqual(expectedResult, actualResult.ToString());
        }

        private Book GetBook()
        {
            return new Book
            {
                Name = "TestBook",
                PublishingYear = 2000,
                Authors = new List<Author>
                {
                    new Author {Name = "Bob", Surname = "Bennett"}
                }
            };
        }

        private NewsPaper GetNewsPaper()
        {
            return new NewsPaper
            {
                Name = "TestNewsPaper",
                PagesCount = 30,
                Date = new DateTime(1990, 12, 12),
                Note = "Test Note"
            };
        }

        private Patent GetPatent()
        {
            return new Patent
            {
                Name = "TestPatent",
                PublishingDate = new DateTime(1980, 1, 1),
                Creators = new List<Author>
                {
                    new Author {Name = "John", Surname = "Smith"},
                    new Author {Name = "Jane", Surname = "Adams"}
                }
            };
        }

        private string GetBookXml()
        {
            return "<book name=\"TestBook\" " +
                   "publishingYear=\"2000\">" +
                   "<authors>" +
                   "<author name=\"Bob\" surname=\"Bennett\" />" +
                   "</authors>" +
                   "</book>";
        }

        private string GetNewspaperXml()
        {
            return "<newspaper name=\"TestNewsPaper\" " +
                   "pagesCount=\"30\" " +
                   "date=\"12/12/1990\">" +
                   "<note>Test Note</note>" +
                   "</newspaper>";
        }

        private string GetPatentXml()
        {
            return "<patent name=\"TestPatent\" " +
                   "publishingDate=\"01/01/1980\">" +
                   "<creators>" +
                   "<creator name=\"John\" surname=\"Smith\" />" +
                   "<creator name=\"Jane\" surname=\"Adams\" />" +
                   "</creators>" +
                   "</patent>";
        }
    }

    internal class BooksComparer : IComparer, IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return x.Name == y.Name
                   && x.PublishingYear == y.PublishingYear ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare(x as Book, y as Book);
        }
    }

    internal class NewsPaperComparer : IComparer, IComparer<NewsPaper>
    {
        public int Compare(NewsPaper x, NewsPaper y)
        {
            return x.Name == y.Name &&
                   x.PagesCount == y.PagesCount &&
                   x.Date == y.Date &&
                   x.Note == y.Note ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare(x as NewsPaper, y as NewsPaper);
        }
    }

    internal class PatentComparer : IComparer, IComparer<Patent>
    {
        public int Compare(Patent x, Patent y)
        {
            return x.Name == y.Name &&
                   x.PublishingDate == y.PublishingDate ? 0 : 1;
        }

        public int Compare(object x, object y)
        {
            return Compare(x as Patent, y as Patent);
        }
    }
}
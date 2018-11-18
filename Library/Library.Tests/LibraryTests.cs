using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass()]
    public class LibraryTests
    {
        private Library _library;

        [TestInitialize]
        public void Initialize()
        {
            _library = new Library();
            _library.AddParsers(new BookParser(), new NewsPaperParser(), new PatentParser());
            _library.AddWriters(new BookWriter(), new NewsPaperWriter(), new PatentWriter());

        }

        [TestMethod()]
        public void LibraryTest()
        {
            TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
                                             "<catalog>" +
                                             GetBookXml() +
                                             "</catalog>");
            IEnumerable<ILibraryItem> books = _library.ReadFrom(sr).ToList();

            //CollectionAssert.AreEqual(books, new[]
            //{
            //    GetBook()
            //}, new BooksComparer());

            sr.Dispose();
        }

        [TestMethod()]
        public void AddParsersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddWritersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadFromTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WriteToTest()
        {
            Assert.Fail();
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
                Date = new DateTime(1990, 4, 20)
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
                   "publishingYear=\"2000\" " +
                   "<authors>" +
                   "<author name=\"Bob\" surname=\"Bennett\" />" +
                   "</authors>" +
                   "</book>";
        }

        private string GetNewspaperXml()
        {
            return "<newspaper name=\"TestNewsPaper\" " +
                   "pagesCount=\"30\" " +
                   "date=\"05/16/1905\" " +
                   "</newspaper>";
        }

        private string GetPatentXml()
        {
            return "<patent name=\"TestPatent\" " +
                   "publishingDate=\"01/01/1980\" " +
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
                   x.Date == y.Date ? 0 : 1;
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
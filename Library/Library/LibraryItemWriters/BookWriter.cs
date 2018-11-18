using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Library.Abstract;
using Library.LibraryItems;

namespace Library.LibraryItemWriters
{
    public class BookWriter : XmlLibraryItemWriter
    {
        public override Type TypeToWrite => typeof(Book);
        public override void WriteLibraryItem(XmlWriter xmlWriter, ILibraryItem libraryItem)
        {
            Book book = libraryItem as Book;
            if (book == null)
            {
                throw new ArgumentException($"{nameof(libraryItem)} can't be null or not of type {nameof(Book)}");
            }

            XElement element = new XElement("book");
            AddAttribute(element, "name", book.Name);
            AddAttribute(element, "publishingYear", book.PublishingYear.ToString());
            AddElement(element, "authors",
                book.Authors?.Select(a => new XElement("author",
                    new XAttribute("name", a.Name),
                    new XAttribute("surname", a.Surname)
                ))
            );
            element.WriteTo(xmlWriter);
        }
    }
}

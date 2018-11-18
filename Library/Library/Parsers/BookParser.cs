using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.Abstract;
using Library.LibraryItems;

namespace Library.Parsers
{
    public class BookParser : XmlElementParser
    {
        public override string ElementName => "book";
        public override ILibraryItem ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} can't be null");
            }

            Book book = new Book
            {
                Name = GetAttributeValue(element, "name"),
                Authors = GetElement(element, "authors").Elements("author").Select(e => new Author
                {
                    Name = GetAttributeValue(e, "name"),
                    Surname = GetAttributeValue(e, "surname")
                }).ToList(),
                PublishingYear = int.Parse(GetAttributeValue(element, "publishingYear")),
            };
            return book;
        }
    }
}

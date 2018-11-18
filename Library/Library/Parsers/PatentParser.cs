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
    public class PatentParser : XmlElementParser
    {
        public override string ElementName => "patent";
        public override ILibraryItem ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} can't be null");
            }

            Patent patent = new Patent
            {
                Name = GetAttributeValue(element, "name"),
                PublishingDate = GetDate(GetAttributeValue(element, "publishingDate")),
                Creators = GetElement(element, "creators").Elements("creator").Select(e => new Author
                {
                    Name = GetAttributeValue(e, "name"),
                    Surname = GetAttributeValue(e, "surname")
                }).ToList()
            };
            return patent;
        }
    }
}

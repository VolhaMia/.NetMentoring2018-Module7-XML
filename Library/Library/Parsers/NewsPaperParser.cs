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
    public class NewsPaperParser : XmlElementParser
    {
        public override string ElementName => "newspaper";
        public override ILibraryItem ParseElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} can't be null");
            }

            NewsPaper newsPaper = new NewsPaper
            {
                Name = GetAttributeValue(element, "name"),
                PagesCount = int.Parse(GetAttributeValue(element, "pagesCount") ?? default(int).ToString()),
                Date = GetDate(GetAttributeValue(element, "date")),
                Note = GetElement(element, "note")?.Value,
            };
            return newsPaper;
        }
    }
}

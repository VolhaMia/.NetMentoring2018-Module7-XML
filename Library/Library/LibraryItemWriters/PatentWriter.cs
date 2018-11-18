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
    public class PatentWriter : XmlLibraryItemWriter
    {
        public override Type TypeToWrite => typeof(Patent);
        public override void WriteLibraryItem(XmlWriter xmlWriter, ILibraryItem libraryItem)
        {
            Patent patent = libraryItem as Patent;
            if (patent == null)
            {
                throw new ArgumentException($"{nameof(libraryItem)} can't be null or not of type {nameof(Patent)}");
            }

            XElement element = new XElement("patent");
            AddAttribute(element, "name", patent.Name);
            AddAttribute(element, "publishingDate", GetDateString(patent.PublishingDate.Date));
            AddElement(element, "creators", patent.Creators.Select(
                c => new XElement("creator",
                    new XAttribute("name", c.Name),
                    new XAttribute("surname", c.Surname))));
            element.WriteTo(xmlWriter);
        }
    }
}

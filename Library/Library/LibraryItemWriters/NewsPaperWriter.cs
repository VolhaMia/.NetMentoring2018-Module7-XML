using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Library.Abstract;
using Library.LibraryItems;

namespace Library.LibraryItemWriters
{
    public class NewsPaperWriter : XmlLibraryItemWriter
    {
        public override Type TypeToWrite => typeof(NewsPaper);
        public override void WriteLibraryItem(XmlWriter xmlWriter, ILibraryItem libraryItem)
        {
            NewsPaper newsPaper = libraryItem as NewsPaper;
            if (newsPaper == null)
            {
                throw new ArgumentException($"{nameof(libraryItem)} can't be null or not of type {nameof(NewsPaper)}");
            }

            XElement element = new XElement("newspaper");
            AddAttribute(element, "name", newsPaper.Name);
            AddAttribute(element, "pagesCount", newsPaper.PagesCount.ToString());
            AddAttribute(element, "date", newsPaper.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture));
            AddElement(element, "note", newsPaper.Note);
            element.WriteTo(xmlWriter);
        }
    }
}

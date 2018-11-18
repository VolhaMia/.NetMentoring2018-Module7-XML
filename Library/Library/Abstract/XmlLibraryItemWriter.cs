using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Library.Exceptions;

namespace Library.Abstract
{
    public abstract class XmlLibraryItemWriter : ILibraryItemWriter
    {
        public abstract Type TypeToWrite { get; }
        public abstract void WriteLibraryItem(XmlWriter xmlWriter, ILibraryItem libraryItem);

        protected string GetDateString(DateTime date)
        {
            return date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture);
        }

        protected void AddAttribute(XElement element, string attributeName, string value, bool isRequired = false)
        {
            if (string.IsNullOrEmpty(value) && isRequired)
            {
                throw new NoRequiredElementException($"Required attribute \"{attributeName}\" can't be null or empty");
            }

            element.SetAttributeValue(attributeName, value);
        }

        protected void AddElement(XElement element, string newElementName, object value, bool isRequired = false)
        {
            if (value == null && isRequired)
            {
                throw new NoRequiredElementException($"Required attribute \"{newElementName}\" can't be null");
            }

            var newElement = new XElement(newElementName, value);
            element.Add(newElement);
        }
    }
}

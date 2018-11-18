using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.Exceptions;

namespace Library.Abstract
{
    public abstract class XmlElementParser : IElementParser
    {
        public abstract string ElementName { get; }
        public abstract ILibraryItem ParseElement(XElement element);

        protected string GetAttributeValue(XElement element, string name, bool isRequired = false)
        {
            var attribute = element.Attribute(name);
            if (string.IsNullOrEmpty(attribute?.Value) && isRequired)
            {
                throw new NoRequiredElementException($"{name}");
            }

            return attribute?.Value;
        }

        protected XElement GetElement(XElement parentElement, string name, bool isRequired = false)
        {
            var element = parentElement.Element(name);
            if (string.IsNullOrEmpty(element?.Value) && isRequired)
            {
                throw new NoRequiredElementException($"{name}");
            }

            return element;
        }

        protected DateTime GetDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return default(DateTime);
            }

            return DateTime.ParseExact(date, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
                CultureInfo.InvariantCulture.DateTimeFormat);
        }
    }
}

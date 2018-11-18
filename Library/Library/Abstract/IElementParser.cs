using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.Abstract
{
    public interface IElementParser
    {
        string ElementName { get; }
        ILibraryItem ParseElement(XElement element);
    }
}

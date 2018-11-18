using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Library.Abstract
{
    public interface ILibraryItemWriter
    {
        Type TypeToWrite { get; }

        void WriteLibraryItem(XmlWriter xmlWriter, ILibraryItem libraryItem);
    }
}

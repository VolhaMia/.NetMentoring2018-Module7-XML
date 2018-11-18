using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Library.Abstract;
using Library.Exceptions;

namespace Library
{
    public class Library
    {
        private static string _catalogElement = "catalog";
        private readonly IDictionary<string, IElementParser> _elementParsers;
        private readonly IDictionary<Type, ILibraryItemWriter> _libraryItemsWriters;

        public Library()
        {
            _elementParsers = new Dictionary<string, IElementParser>();
            _libraryItemsWriters = new Dictionary<Type, ILibraryItemWriter>();
        }

        public void AddParsers(params IElementParser[] elementParsers)
        {
            foreach (var parser in elementParsers)
            {
                _elementParsers.Add(parser.ElementName, parser);
            }
        }

        public void AddWriters(params ILibraryItemWriter[] writers)
        {
            foreach (var writer in writers)
            {
                _libraryItemsWriters.Add(writer.TypeToWrite, writer);
            }
        }

        public IEnumerable<ILibraryItem> ReadFrom(TextReader input)
        {
            using (XmlReader xmlReader = XmlReader.Create(input, new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            }))
            {
                xmlReader.ReadToFollowing(_catalogElement);
                xmlReader.ReadStartElement();

                do
                {
                    while (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        var node = XNode.ReadFrom(xmlReader) as XElement;
                        IElementParser parser;
                        if (_elementParsers.TryGetValue(node.Name.LocalName, out parser))
                        {
                            yield return parser.ParseElement(node);
                        }
                        else
                        {
                            throw new UnknownElementTagException($"Unknown element tag found: {node.Name.LocalName}");
                        }
                    }
                }
                while (xmlReader.Read());
            }
        }

        public void WriteTo(TextWriter output, IEnumerable<ILibraryItem> catalogEntities)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(output, new XmlWriterSettings()))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(_catalogElement);
                foreach (var catalogEntity in catalogEntities)
                {
                    ILibraryItemWriter writer;
                    if (_libraryItemsWriters.TryGetValue(catalogEntity.GetType(), out writer))
                    {
                        writer.WriteLibraryItem(xmlWriter, catalogEntity);
                    }
                    else
                    {
                        throw new LibraryItemNotFoundException($"Library item writer for type {catalogEntity.GetType().FullName} not found");
                    }
                }
                xmlWriter.WriteEndElement();
            }
        }
    }
}

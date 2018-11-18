using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Abstract;

namespace Library.LibraryItems
{
    public class Book : ILibraryItem
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public int PublishingYear { get; set; }
    }
}

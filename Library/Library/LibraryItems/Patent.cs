using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Abstract;

namespace Library.LibraryItems
{
    public class Patent : ILibraryItem
    {
        public string Name { get; set; }
        public List<Author> Creators { get; set; }
        public DateTime PublishingDate { get; set; }
    }
}

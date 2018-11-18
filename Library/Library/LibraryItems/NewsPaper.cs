using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Abstract;

namespace Library.LibraryItems
{
    public class NewsPaper : ILibraryItem
    {
        public string Name { get; set; }

        public int PagesCount { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBook.Models
{
    public class OrderView
    {
        public List<Order> order { get; set; }
        public List<Book> book { get; set; }

        public List<Invoice> invoice { get; set; }

    }
}
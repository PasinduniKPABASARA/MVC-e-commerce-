using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBook.Models
{
    public class Cart
    {
        public int bookId { get; set; }
        public string bookName { get; set; }
        public double bookPrice { get; set; }

        public Nullable<int> orderQty { get; set; }
        public Nullable<double> orderBill { get; set; }
    }
}
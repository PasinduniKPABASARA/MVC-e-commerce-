using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBook.Models
{
    public class adViewModel
    {
        public int bookId { get; set; }
        public string bookName { get; set; }
        public string bookAuthor { get; set; }
        public string bookDescription { get; set; }
        public double bookPrice { get; set; }
        public string bookImage { get; set; }
        public Nullable<int> categoryId { get; set; }

        public string categoryName { get; set; }

        public int feddbackId { get; set; }
        public string feedbackDescription { get; set; }
        public System.DateTime feedbackDate { get; set; }

        public Nullable<int> userId { get; set; }

        public string userName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBook.Models
{
    public class feed
    {
        public int feddbackId { get; set; }
        public string feedbackDescription { get; set; }
        public System.DateTime feedbackDate { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> bookId { get; set; }

     
    }
}
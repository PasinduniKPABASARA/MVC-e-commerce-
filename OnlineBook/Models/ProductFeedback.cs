using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBook.Models
{
    public class ProductFeedback
    {
        public adViewModel aVM { get; set; }
        public IPagedList<Feedback> fdb { get; set; }
    }
}
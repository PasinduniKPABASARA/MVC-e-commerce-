using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBook.Models
{
    public class InvoiceDisplay
    {
      public Invoice invoice { get; set; }
      public User user { get; set; }
    }
}
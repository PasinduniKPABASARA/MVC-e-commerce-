//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineBook.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int orderId { get; set; }
        public System.DateTime orderDate { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> bookId { get; set; }
        public Nullable<int> invoiceId { get; set; }
        public Nullable<int> orderQty { get; set; }
        public Nullable<double> orderBill { get; set; }
        public Nullable<double> orderUnitPrice { get; set; }
        public string bookName { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual User User { get; set; }
    }
}
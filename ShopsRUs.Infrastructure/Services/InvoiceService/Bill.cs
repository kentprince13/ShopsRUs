using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Infrastructure.Services.InvoiceService
{
    public class Bill
    {
        public string Item { get; set; }
        public decimal Amount { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserName { get; set; }
        public string PreferredDiscountName { get; set; }
        public BillsType BillsType { get; set; }

    }

    public enum BillsType
    {
        Groceries,
        Others
    }
}

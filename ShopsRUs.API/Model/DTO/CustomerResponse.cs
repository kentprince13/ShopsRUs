using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Domain.Enum;
using ShopsRUs.Domain.Infrastructure;

namespace ShopsRUs.API.Model.DTO
{
    public class CustomerResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
    } 
    
    public class DiscountResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
    } 
    
    public class InvoiceResponse
    {
        public long Id { get; set; }
        public string Item { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalAMountPaid { get; set; }
        public decimal DiscountedAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserName { get; set; }
    }

    public class CustomerRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    } 
    
    public class BillRequest
    {
        public string Item { get; set; }
        public decimal Amount { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserName { get; set; }
        public string PreferredDiscountName { get; set; }
        public string BillsType { get; set; }
    } 
    
    public class DiscountRequest
    {
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public string Value { get; set; }
    }
}

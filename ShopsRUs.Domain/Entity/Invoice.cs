using System;

namespace ShopsRUs.Domain.Entity
{
    public class Invoice
    {
        public long Id { get; set; }
        public string Item { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalAMountPaid { get; set; }
        public decimal DiscountedAmount { get; set; }
        public Discount Discount { get; set; }
        public long DiscountId { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
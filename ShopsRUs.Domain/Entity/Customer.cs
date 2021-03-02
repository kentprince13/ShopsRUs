using System;

namespace ShopsRUs.Domain.Entity
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public decimal TotalAMountSpent { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public DateTime LastVisited { get; set; }
    }
}

using System;
using ShopsRUs.Domain.Enum;
using ShopsRUs.Domain.Infrastructure;

namespace ShopsRUs.Domain.Entity
{
    public class Discount
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public DiscountTypes DiscountTypes
        {
            get => DiscountType.ParseEnum<DiscountTypes>();
            set => DiscountType = value.ToString();
        }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
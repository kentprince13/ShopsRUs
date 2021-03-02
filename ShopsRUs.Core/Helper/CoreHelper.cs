using System;
using System.Collections.Generic;
using System.Text;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Core.Helper
{
    public static class CoreHelper
    {
        public static decimal GetIntDiscountValue(this Discount discount)
        {
            return Convert.ToInt16(discount.Value);
        }
    }
}
    
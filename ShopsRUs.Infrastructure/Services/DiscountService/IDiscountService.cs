using System.Collections.Generic;
using System.Threading.Tasks;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<List<Discount>> GetAllDiscounts();
        Task<Discount> GetDiscountById(long id);
        Task<Discount> GetDiscountByName(string name);
        Task<Discount> GetDiscountByNameAndType(string name, string type);
        Task CreateDiscount(Discount discounts);
    }
}
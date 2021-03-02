using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Entity;


namespace ShopsRUs.Infrastructure.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly ShopsRUsContext _context;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(ShopsRUsContext context, ILogger<DiscountService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Discount>> GetAllDiscounts()
        {
            _logger.LogInformation("Fetching all Discount");
            var discounts =  await _context.Discounts.ToListAsync();
            return discounts;
        } 
        
        public async Task<Discount> GetDiscountById(long id)
        {
            _logger.LogInformation($"Fetching Discount by Id: {id}");
            var discounts =  await _context.Discounts.FirstOrDefaultAsync(c=>c.Id == id);
            return discounts;
        } 
        public async Task<Discount> GetDiscountByName(string name)
        {
            _logger.LogInformation($"Fetching Discount by Name: {name}");
            var discounts =  await _context.Discounts.FirstOrDefaultAsync(c=>c.Name == name);
            return discounts;
        }

        public async Task<Discount> GetDiscountByNameAndType(string name, string type)
        {
            _logger.LogInformation($"Fetching Discount by Name: {name}");
            var discounts = await _context.Discounts.FirstOrDefaultAsync(c => c.Name == name && c.DiscountType == type);
            return discounts;
        }
        public async Task CreateDiscount(Discount discounts)
        {

            await _context.Discounts.AddAsync(discounts);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Discount Created with Name: {discounts.Name}");

        }
    }
}
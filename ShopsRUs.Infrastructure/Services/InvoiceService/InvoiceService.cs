using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Infrastructure.Services.DiscountService;

namespace ShopsRUs.Infrastructure.Services.InvoiceService
{
    public class InvoiceService : IInvoiceServices
    {
        private readonly ShopsRUsContext _context;
        private readonly ILogger<InvoiceService> _logger;
        public InvoiceService(ShopsRUsContext context, ILogger<InvoiceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            _logger.LogInformation("Fetching all Invoices");
            var discounts =  await _context.Invoices.ToListAsync();
            return discounts;
        } 
        
        public async Task<Invoice> GetInvoiceById(long id)
        {
            _logger.LogInformation($"Fetching Invoice by Id: {id}");
            var discounts =  await _context.Invoices.FirstOrDefaultAsync(c=>c.Id == id);
            return discounts;
        }

        public async Task<Invoice> GetInvoiceByItemName(string name)
        {
            _logger.LogInformation($"Fetching Invoice by Name: {name}");
            var discounts =  await _context.Invoices.FirstOrDefaultAsync(c=>c.Item == name);
            return discounts;
        } 
        public async Task CreateInvoice(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Discount Created with Name: {invoice.Item}");

        }
    }
}
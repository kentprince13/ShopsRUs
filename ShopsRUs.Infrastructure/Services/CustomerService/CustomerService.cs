using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ShopsRUsContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ShopsRUsContext context, ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<List<Customer>> GetCustomers()
        {
            _logger.LogInformation("Fetching all Customers");
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerById(long id)
        {
            _logger.LogInformation($"Fetching Customer by Id: {id}");
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }
        public async Task<Customer> GetCustomerByName(string name)
        {
            _logger.LogInformation($"Fetching Customer by Name: {name}");
            var customer = await _context.Customers.Where(c => c.Name == name).FirstOrDefaultAsync();
            return customer;
        }
        public async Task CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Customer Created with Name: {customer.Name}");
        }
    }
}

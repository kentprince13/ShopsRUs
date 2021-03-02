using System.Collections.Generic;
using System.Threading.Tasks;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(long id);
        Task<Customer> GetCustomerByName(string name);
        Task CreateCustomer(Customer customer);
    }
}
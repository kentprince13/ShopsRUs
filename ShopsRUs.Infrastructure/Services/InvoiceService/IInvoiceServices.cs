using System.Collections.Generic;
using System.Threading.Tasks;
using ShopsRUs.Domain.Entity;

namespace ShopsRUs.Infrastructure.Services.InvoiceService
{
    public interface IInvoiceServices
    {
        Task<List<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoiceById(long id);
        Task<Invoice> GetInvoiceByItemName(string name);
        Task CreateInvoice(Invoice discounts);
    }
}
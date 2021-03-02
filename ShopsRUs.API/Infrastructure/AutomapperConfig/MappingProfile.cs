using AutoMapper;
using ShopsRUs.API.Model.DTO;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Domain.Infrastructure;
using ShopsRUs.Infrastructure.Services.InvoiceService;

namespace ShopsRUs.API.Infrastructure.AutomapperConfig
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResponse>();
            CreateMap<Discount, DiscountResponse>();
            CreateMap<Invoice, InvoiceResponse>();
            CreateMap<BillRequest, Bill>()
                .ForMember(c=>c.BillsType, d=>d.MapFrom(c=>c.BillsType.ParseEnum<BillsType>()));
        }
    }
}

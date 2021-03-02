using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopsRUs.Core.Exceptions;
using ShopsRUs.Core.Helper;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Domain.Enum;
using ShopsRUs.Infrastructure.Services.DiscountService;
using ShopsRUs.Infrastructure.Services.InvoiceService;
using ShopsRUs.Infrastructure.Services.UserService;

namespace ShopsRUs.Core.Infrastructure
{
    public class InvoicingService: IInvoicingService
    {
        private readonly IUsersService _usersService;
        private readonly IDiscountService _discountService;
        private readonly ILogger<InvoicingService> _logger;

        public InvoicingService(IUsersService usersService, IDiscountService discountService,
            ILogger<InvoicingService> logger)
        {
            _usersService = usersService;
            _discountService = discountService;
            _logger = logger;
        }

        private const string DEFAULT_DISCOUNT = "Default";
        public async Task<Invoice> ComputeInvoiceAMount(Bill bill)
        {

            var user = await _usersService.GetUserByNamAndPhone(bill.UserName, bill.UserPhoneNumber);

            Discount discount;
            decimal discountedFlatAmount = 0m,
                discountedPercentageAmount = 0m,
                totalDiscountedAmount = 0m;
            if (user == null)
            {
                throw new NotFoundException( "user Not Found");
            }

            // check if discount is groceries type
            if (bill.BillsType == BillsType.Groceries)
            {
                // check if discount if it has preferred discount
                if (!string.IsNullOrEmpty(bill.PreferredDiscountName))
                {
                    // check if preferred discount exist or use default
                    discount = await _discountService.GetDiscountByName(bill.PreferredDiscountName);
                    if (discount != null)
                    {
                        //if preferred discount is flat or percentage based
                        if (discount.DiscountTypes == DiscountTypes.Flat)
                        {
                            discountedFlatAmount += Convert.ToDecimal(discount.Value);
                        }
                        else
                        {
                            discountedPercentageAmount += GetPercentageDiscount(discount.GetIntDiscountValue(), bill.Amount);
                        }
                    }
                    else
                    {
                        discount = await _discountService.GetDiscountByName(DEFAULT_DISCOUNT);
                        discountedFlatAmount += Math.Floor(bill.Amount / 100) * discount.GetIntDiscountValue();
                    }

                }
                else
                {
                    discount = await _discountService.GetDiscountByName(DEFAULT_DISCOUNT);
                    discountedFlatAmount += Math.Floor(bill.Amount / 100) * discount.GetIntDiscountValue();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(bill.PreferredDiscountName))
                {
                    // check if preferred discount exist or use default
                    discount = await _discountService.GetDiscountByName(bill.PreferredDiscountName);

                    if (discount != null)
                    {
                        //if preferred discount is flat or percentage based
                        if (discount.DiscountTypes == DiscountTypes.Flat)
                        {
                            discountedFlatAmount += Convert.ToDecimal(discount.Value);
                        }
                        else
                        {
                            discountedPercentageAmount += GetPercentageDiscount(discount.GetIntDiscountValue(), bill.Amount); 
                        }
                    }
                }
                else
                {
                    discount = await _discountService.GetDiscountByName(DEFAULT_DISCOUNT);
                    discountedFlatAmount += Math.Floor(bill.Amount / 100) * discount.GetIntDiscountValue();
                }

             
                discount = await _discountService.GetDiscountByName(user.UserType);
                var greaterThanTwoYears = (user.CreatedOn.AddYears(2) < DateTime.Now);

                switch (user.UsersType)
                {
                    case UsersType.Affiliate:
                    case UsersType.Employee:
                        discountedPercentageAmount += GetPercentageDiscount(discount.GetIntDiscountValue(), bill.Amount);
                        break;

                    case UsersType.Customer:
                        discountedPercentageAmount += greaterThanTwoYears ? 
                            GetPercentageDiscount(discount.GetIntDiscountValue(), bill.Amount): 0.0m;
                        break;

                    default:
                        discountedPercentageAmount += 0.0m;
                        break;
                }
                
            }

            totalDiscountedAmount = discountedFlatAmount + discountedPercentageAmount;

            var invoice = new Invoice
            {
                UserId = user.Id,
                CreatedOn = DateTime.Now,
                DiscountedAmount = totalDiscountedAmount,
                Discount = discount ,
                TotalCost = bill.Amount,
                Item = bill.Item,
                TotalAMountPaid = bill.Amount - totalDiscountedAmount
            };
             
            return invoice;
        }

        private Decimal GetPercentageDiscount(decimal value, decimal billAmount)
        {
            decimal discountPercentage = value / 100;
            return discountPercentage * billAmount;
        }
    }

    public interface IInvoicingService
    {
        Task<Invoice> ComputeInvoiceAMount(Bill bill);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ShopsRUs.API.Infrastructure.Validation;
using ShopsRUs.API.Model.DTO;
using ShopsRUs.Domain.Enum;
using ShopsRUs.Infrastructure.Services.InvoiceService;

namespace ShopsRUs.API.Validators
{
    public class CustomerValidator:NullReferenceValidator<CustomerRequest>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name Must Not Be Empty");
            RuleFor(c => c.PhoneNumber).NotEmpty().Length(11).WithMessage("Phone Number Must be 11 Character And Not Empty");

        }
    } 
    
    public class BillRequestValidator : NullReferenceValidator<BillRequest>
    {
        public BillRequestValidator()
        {
            RuleFor(c => c.Item).NotEmpty().WithMessage("Item Must Not Be Empty");
            RuleFor(c => c.UserPhoneNumber).NotEmpty().WithMessage("User Phone Number Must Not Be Empty");
            RuleFor(c => c.UserName).NotEmpty().WithMessage("User Name Must Not Be Empty");
            RuleFor(c => c.Amount).NotEmpty().GreaterThan(0).WithMessage("Item Must Be Greater Than Zero");
            RuleFor(c => c.BillsType).NotEmpty().IsEnumName(typeof(BillsType), false)
                .WithMessage("Invalid Bills Type, Supported Bills Types Include: Groceries and Others");
        }
    }
    
    public class DiscountRequestValidator : NullReferenceValidator<DiscountRequest>
    {
        public DiscountRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name Must Not Be Empty");
            RuleFor(c => c.DiscountType).NotEmpty().IsEnumName(typeof(DiscountTypes),false).WithMessage("Invalid Discount Type, Supported Discount Types Include: Flat and Percentage");
            RuleFor(c => c.Value).NotEmpty().MaximumLength(3).WithMessage("Value Must Not Be Empty");
        }
    }
}

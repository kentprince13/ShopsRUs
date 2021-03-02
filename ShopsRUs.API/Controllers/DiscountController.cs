using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ShopsRUs.API.Model;
using ShopsRUs.API.Model.DTO;
using ShopsRUs.API.Validators;
using ShopsRUs.Core.Exceptions;
using ShopsRUs.Domain.Entity;
using ShopsRUs.Infrastructure.Services.DiscountService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopsRUs.API.Controllers
{
    [Route("api/v1/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountController> _logger;
        private readonly IDiscountService _discountService;

        public DiscountController(IMapper mapper,
            ILogger<DiscountController> logger,
            IDiscountService discountService)
        {
            _mapper = mapper;
            _logger = logger;
            _discountService = discountService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DiscountResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching All Discount");

            try
            {
                var discounts  = await _discountService.GetAllDiscounts();
                if (discounts == null)
                {
                    throw new NotFoundException("NOT FOUND", "Discount Not Found");
                }

                var response = _mapper.Map<List<DiscountResponse>>(discounts);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while retrieving all Discount --> message {e.Message}");
                throw;
            }
        }


        [HttpGet("{name}")]
        [ProducesResponseType(typeof(DiscountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FetchCustomerByName([FromRoute] string name)
        {
            try
            {
                var discount = await _discountService.GetDiscountByName(name);
                if (discount == null)
                {
                    throw new NotFoundException("NOT FOUND", "Discount Not Found");
                }
                var response = _mapper.Map<DiscountResponse>(discount);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while retrieving Discount : {name} --> message {e.Message}");
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create([FromBody] DiscountRequest request)
        {
            try
            {
                var validator = new DiscountRequestValidator();
                var result = await validator.ValidateAsync(request);
                if (result.IsValid)
                {
                    var existingCustomer = await _discountService.GetDiscountByName(request.Name);
                    if (existingCustomer != null)
                    {
                        throw new BadRequestException("Discount Already Exist For this Discount Type");
                    }

                    var discount = new Discount
                    {
                        DiscountType = request.DiscountType,
                        CreatedOn = DateTime.Now,
                        Value = request.Value,
                        Name = request.Name
                    };

                    await _discountService.CreateDiscount(discount);

                    var response = _mapper.Map<DiscountResponse>(discount);

                    return CreatedAtAction(nameof(Get), new { name = discount.Name }, response);
                }


                throw new ValidationException(result.Errors.ToList());
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while Creating Discount --> message {e.Message}");
                throw;
            }

        }
    }
}

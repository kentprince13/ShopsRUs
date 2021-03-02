using Microsoft.AspNetCore.Http;
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
using ShopsRUs.Core.Infrastructure;
using ShopsRUs.Infrastructure.Services.InvoiceService;

namespace ShopsRUs.API.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoicingService _invoicingService;

        public InvoiceController(IMapper mapper,
            ILogger<InvoiceController> logger,
            IInvoicingService invoicingService)
        {
            _mapper = mapper;
            _logger = logger;
            _invoicingService = invoicingService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(InvoiceResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GenerateInvoice([FromBody] BillRequest request)
        {
            try
            {
                var validator = new BillRequestValidator();
                var result = await validator.ValidateAsync(request);
                if (result.IsValid)
                {
                    var bills = _mapper.Map<Bill>(request);
                    var computeInvoice = await _invoicingService.ComputeInvoiceAMount(bills);
                    var invoice = _mapper.Map<InvoiceResponse>(computeInvoice);
                    invoice.UserName = request.UserName;
                    invoice.UserPhoneNumber = request.UserPhoneNumber;
                    
                    return Ok(invoice);
                }

                throw new ValidationException(result.Errors.ToList());
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while Generating Invoice --> message {e.Message}");
                throw;
            }

        }

    }
}

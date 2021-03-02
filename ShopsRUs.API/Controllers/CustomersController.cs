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
using ShopsRUs.Domain.Enum;
using ShopsRUs.Infrastructure.Services.CustomerService;
using ShopsRUs.Infrastructure.Services.UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopsRUs.API.Controllers
{
    [Route("api/v1/customer")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public CustomersController(ILogger<CustomersController> logger, 
            ICustomerService customerService, IUsersService usersService,IMapper mapper)
        {
            _logger = logger;
            _customerService = customerService;
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching All Customers");

            try
            {
                var customers = await _customerService.GetCustomers();
                if (customers == null)
                {
                    throw new NotFoundException("NOT FOUND", "Customer Not Found");
                }

                var response = _mapper.Map<List<CustomerResponse>>(customers);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while retrieving all customer --> message {e.Message}");
                throw;
            }
        }

        [HttpGet, Route("{id:int}")]
        [ProducesResponseType(typeof(CustomerResponse),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse),(int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    throw new NotFoundException("NOT FOUND", "Customer Not Found");
                }

                var response = _mapper.Map<CustomerResponse>(customer);
                return Ok(response);
            }
            catch (Exception e)
            {
               _logger.LogInformation($"Error Occur while retrieving customer : {id} --> message {e.Message}");
               throw;
            }
          
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FetchCustomerByName([FromRoute] string name)
        {
            try
            {
                var customer = await _customerService.GetCustomerByName(name);
                if (customer == null)
                {
                    throw new NotFoundException("NOT FOUND", "Customer Not Found");
                }
                var response = _mapper.Map<CustomerResponse>(customer);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while retrieving customer : {name} --> message {e.Message}");
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create ([FromBody] CustomerRequest request)
        {
            try
            {
                var validator = new CustomerValidator();
                var result = await validator.ValidateAsync(request);
                if (result.IsValid)
                {
                    var existingCustomer = await _customerService.GetCustomerByName(request.Name);
                    if (existingCustomer != null)
                    {
                        throw new BadRequestException("customer Already Exist");
                    }

                    var existingUser = await _usersService.GetUserByNamAndPhone(request.Name, request.PhoneNumber);
                    if (existingUser != null)
                    {
                        throw new BadRequestException("User Already Exist");
                    }

                    var user = new User
                    {
                        Address = request.Address,
                        CreatedOn = DateTime.Now,
                        Email = request.Email,
                        DateOfBirth = DateTime.Now,
                        Name = request.Name,
                        PhoneNumber = request.Name,
                        IsActive = true,
                        UserType = UsersType.Customer.ToString()
                    };

                    await _usersService.CreateUser(user);

                    var customer = new Customer
                    {
                        Address = request.Address,
                        CreatedOn = DateTime.Now,
                        Email = request.Email,
                        LastVisited = DateTime.Now,
                        Name = request.Name,
                        PhoneNumber = request.PhoneNumber,
                        User = user
                    };

                    await _customerService.CreateCustomer(customer);

                    var response = _mapper.Map<CustomerResponse>(customer);

                    return CreatedAtAction(nameof(Get), new { id = customer.Id }, response);
                }


                throw new ValidationException(result.Errors.ToList());
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Error Occur while Creating customer --> message {e.Message}");
                throw;
            }

        }
    }
}

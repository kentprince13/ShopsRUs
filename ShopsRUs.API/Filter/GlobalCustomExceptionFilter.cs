using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopsRUs.API.Extensions;
using ShopsRUs.API.Model;
using ShopsRUs.Core.Exceptions;
using Formatting = System.Xml.Formatting;

namespace ShopsRUs.API.Filter
{
    public class GlobalCustomExceptionFilter:IExceptionFilter
    {
        private readonly ILogger<GlobalCustomExceptionFilter> _logger;

        public GlobalCustomExceptionFilter(ILogger<GlobalCustomExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode;
            ErrorResponse response;
            switch (context.Exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = notFoundException.ToErrorResponse();
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = badRequestException.ToErrorResponse();
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = context.Exception.ToErrorResponse();
                    break;
            }

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            var result = JsonConvert.SerializeObject(response, serializerSettings);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.WriteAsync(result);
            context.ExceptionHandled = true;
        }
    }
}

using System;
using ShopsRUs.API.Model;
using ShopsRUs.Core.Exceptions;

namespace ShopsRUs.API.Extensions
{
    public static class ErrorResponseExtension
    {
        public static ErrorResponse ToErrorResponse(this NotFoundException e)
        {
            return new ErrorResponse()
            {
                Code = e.Code,
                Message = e.Message
            };
        }

        public static ErrorResponse ToErrorResponse(this BadRequestException e)
        {
            return new ErrorResponse()
            {
                Code = e.Code,
                Message = e.Message
            };
        }

        public static ErrorResponse ToErrorResponse(this Exception e)
        {
            return new ErrorResponse()
            {
                Code = "SYSTEM_ERROR",
                Message = "Unexpected error occured please try again or confirm current operation status"
            };
        }
    }
}

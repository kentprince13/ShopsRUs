﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace ShopsRUs.API.Infrastructure.Validation
{
    public class NullReferenceValidator<T>:AbstractValidator<T>
    {
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            return context.InstanceToValidate == null
                ? new ValidationResult(new[] { new ValidationFailure("Request", "Request cannot be empty", "Error") })
                : base.Validate(context);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = new CancellationToken())
        {
            return context.InstanceToValidate == null
                ? Task.FromResult(new ValidationResult(new[] { new ValidationFailure("Request", "Request cannot be empty", "Error") }))
                : base.ValidateAsync(context, cancellation);
        }
    }
}

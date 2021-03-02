using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace ShopsRUs.API.Infrastructure.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("validation error occur")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();
                Failures.Add(propertyName, propertyFailures);
            }
        }
        public IDictionary<string, string[]> Failures { get; set; }
    }
}
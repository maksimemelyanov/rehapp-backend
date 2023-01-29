using System.ComponentModel.DataAnnotations;

namespace Template.Infrastructure.Common;

public static class AttributeValidator
{
    public static ValidationResult Validate<T>(T value)
    {
        if (value is null) throw new ArgumentException("Value was null.");

        var errors = new List<ValidationResult>();

        _ = !System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
            instance: value,
            validationContext: new ValidationContext(value),
            validationResults: errors,
            validateAllProperties: true);

        return new ValidationResult(string.Join(" ", errors));
    }
}

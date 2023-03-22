using System.ComponentModel.DataAnnotations;

namespace RehApp.Infrastructure.Common;

public static class AttributeValidator
{
    public static ValidationResult Validate<T>(T value)
    {
        if (value is null) throw new ArgumentException("Value was null.");

        var errors = new List<ValidationResult>();

        _ = !Validator.TryValidateObject(
            instance: value,
            validationContext: new ValidationContext(value),
            validationResults: errors,
            validateAllProperties: true);

        return new ValidationResult(string.Join(" ", errors));
    }
}

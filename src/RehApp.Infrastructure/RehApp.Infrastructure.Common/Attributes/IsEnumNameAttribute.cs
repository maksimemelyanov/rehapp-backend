using System.ComponentModel.DataAnnotations;

namespace RehApp.Infrastructure.Common.Attributes;

public class IsEnumNameAttribute : ValidationAttribute
{
    private readonly Type type;

    public IsEnumNameAttribute(Type type)
    {
        this.type = type;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        return !type.IsEnum
            ? new ValidationResult("It is necessary to pass 'Enum'.")
            : !Enum.GetNames(type).Any(x => x == (string)value)
                ? new ValidationResult($"There is no such name for enum '{type.Name}'.")
                : ValidationResult.Success;
    }
}
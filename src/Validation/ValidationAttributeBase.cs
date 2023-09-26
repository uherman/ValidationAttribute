using System.ComponentModel.DataAnnotations;

namespace Validation;

/// <summary>
/// Represents the base class for validation attributes,
/// providing a way to specify conditions and error messages for validation.
/// </summary>
public abstract class ValidationAttributeBase : ValidationAttribute
{
    /// <summary>
    /// Gets or sets a value indicating whether an exception should be thrown when validation fails.
    /// </summary>
    public bool ThrowException { get; set; }

    /// <summary>
    /// Gets the validation condition to be applied.
    /// This must be overridden by derived classes to provide the actual condition logic.
    /// </summary>
    protected abstract Func<string, bool> ValidationCondition { get; }

    /// <summary>
    /// Gets the default error message to be used when validation fails.
    /// Derived classes can override this to provide a specific error message.
    /// </summary>
    protected virtual string DefaultErrorMessage => null;

    /// <summary>
    /// Evaluates the condition against the provided value and determines whether it is valid.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>A <see cref="ValidationResult"/> that encapsulates the result of the validation.</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var stringValue = value?.ToString();
        var errorMessage = ErrorMessage ?? DefaultErrorMessage ?? $"{validationContext.DisplayName} is invalid.";

        var isValid = ValidationCondition(stringValue);
        if (!isValid && ThrowException)
        {
            throw new ValidationException(errorMessage);
        }

        return isValid ? ValidationResult.Success : new ValidationResult(errorMessage);
    }
}
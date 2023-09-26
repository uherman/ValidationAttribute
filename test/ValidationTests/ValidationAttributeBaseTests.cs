using System.ComponentModel.DataAnnotations;
using Validation;

namespace ValidationTests;

public class ValidationAttributeBaseTests
{
    [Fact]
    public void IsValid_ReturnsSuccess_WhenValueIsValid()
    {
        // Arrange
        var attribute = new SampleValidationAttribute();
        var sample = new SampleClass { TestProperty = "ValidValue" };
        var validationContext = new ValidationContext(sample) { MemberName = nameof(SampleClass.TestProperty) };

        // Act
        var result = attribute.GetValidationResult(sample.TestProperty, validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ReturnsError_WhenValueIsInvalid()
    {
        // Arrange
        var attribute = new SampleValidationAttribute();
        var sample = new SampleClass { TestProperty = string.Empty };
        var validationContext = new ValidationContext(sample) { MemberName = nameof(SampleClass.TestProperty) };

        // Act
        var result = attribute.GetValidationResult(sample.TestProperty, validationContext);

        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ThrowsException_WhenValueIsInvalid_AndThrowExceptionIsTrue()
    {
        // Arrange
        var attribute = new SampleValidationAttribute { ThrowException = true };
        var sample = new SampleClass { TestProperty = string.Empty };
        var validationContext = new ValidationContext(sample) { MemberName = nameof(SampleClass.TestProperty) };

        // Act & Assert
        Assert.Throws<ValidationException>(() => attribute.GetValidationResult(sample.TestProperty, validationContext));
    }
}

#region Sample Classes

internal class SampleValidationAttribute : ValidationAttributeBase
{
    protected override Func<string, bool> ValidationCondition => value => !string.IsNullOrEmpty(value);
}

internal class SampleClass
{
    [SampleValidation]
    public string TestProperty { get; set; }
}

#endregion
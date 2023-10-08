# ValidationAttributeBase Class

## Overview

`ValidationAttributeBase` is an abstract class located in the `Validation` namespace. It serves as the base class for
validation attributes, extending the functionality of `ValidationAttribute`
from `System.ComponentModel.DataAnnotations`.

## Features

- **ThrowException Property**: Determines whether an exception is thrown when validation fails.
- **ValidationCondition Method**: Abstract method to be overridden by derived classes to provide specific condition
  logic.
- **DefaultErrorMessage Property**: Holds the default error message to be used when validation fails, allowing derived
  classes to provide specific error messages.
- **IsValid Method**: Evaluates the provided value against the condition and returns a `ValidationResult` that
  encapsulates the result of the validation.

## Usage

### Defining a Custom Validation Attribute

Create a derived class and override the `ValidationCondition` to define the validation logic.

```csharp
namespace Validation
{
    public class CustomValidationAttribute : ValidationAttributeBase
    {
        protected override Func<string, bool> ValidationCondition => value => !string.IsNullOrEmpty(value);

        protected override string DefaultErrorMessage => "Value cannot be null or empty";
    }
}
```

### Applying the Custom Validation Attribute

Apply the custom validation attribute to properties or fields of your classes.

```csharp
public class SampleClass
{
    [CustomValidation]
    public string SampleProperty { get; set; }
}
```

### Configuration

Set `ThrowException` to `true` if an exception should be thrown when the validation fails.

```csharp
[CustomValidation(ThrowException = true)]
public string SampleProperty { get; set; }
```

## API

### Properties

- **bool ThrowException { get; set; }**
- **string DefaultErrorMessage { get; }**

### Methods

- **Func<string, bool> ValidationCondition { get; }**
- **ValidationResult IsValid(object value, ValidationContext validationContext)**

## Conclusion

`ValidationAttributeBase` allows for the creation of custom validation attributes with configurable conditions, error
messages, and exception handling, providing enhanced validation capabilities in the .NET ecosystem.

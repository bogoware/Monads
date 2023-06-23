// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using FluentValidationSample;

var validProductNameResult = ProductName.Create("My Product");
Debug.Assert(validProductNameResult.IsSuccess);


var invalidProductNameResult = ProductName.Create("This product name exceeds the maximum length allowed");
Debug.Assert(invalidProductNameResult.IsFailure);

// This is just for demonstration purposes.
// In a real application, you would use the methods provided by the Result<TValue> struct
// to define the behavior.
var error = invalidProductNameResult.GetErrorOrThrow();

switch (error)
{
	case ValidationError validationError:
		foreach (var f in validationError.Failures)
		{
			Console.WriteLine($"Property [{f.PropertyName}] failed with message: {f.ErrorMessage}");
		}
		break;
}
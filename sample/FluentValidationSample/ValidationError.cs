using FluentValidation.Results;

namespace FluentValidationSample;

/// <summary>
/// Represents an error that occurs during the validation of an object.
/// </summary>
public class ValidationError : ApplicationError
{
	private readonly List<ValidationFailure> _failures = new();
	
	/// <summary>
	/// Returns a list of <see cref="ValidationFailure"/>s.
	/// </summary>
	public IReadOnlyList<ValidationFailure> Failures => _failures;
	
	/// <summary>
	/// The type of the object that failed validation.
	/// </summary>
	public Type ObjectType { get; }
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ValidationError"/> class.
	/// </summary>
	public ValidationError(
		string message,
		Type type,
		IEnumerable<ValidationFailure> failures)
		: base(message)
	{
		ObjectType = type;
		_failures.AddRange(failures);
	}
	
	/// <inheritdoc cref="ValidationError"/>
	public ValidationError(
		Type type,
		IEnumerable<ValidationFailure> failures)
		: this($"Error validating {type.Name}", type, failures)
	{
	}
}
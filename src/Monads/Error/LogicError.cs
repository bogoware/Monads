// ReSharper disable MemberCanBeProtected.Global
namespace Bogoware.Monads;

/// <summary>
/// This class can be further inherited to model specific domain error needs.
/// </summary>
/// <example>
/// public class DomainError: LogicError
/// {
/// 	public enum ErrorSeverity
/// 	{
/// 		Low,
/// 		Medium,
/// 		High
/// 	}
/// 	
/// 	public int Code { get; }
/// 	public ErrorSeverity Severity { get; }
/// 	public DomainError(string message, int code, ErrorSeverity severity = ErrorSeverity.Low)
/// 		: base(message)
/// 	{
/// 		Code = code;
/// 		Severity = severity;
/// 	}
/// }
/// </example>
public class LogicError: Error
{
	public override string Message { get; }

	public LogicError(string message)
	{
		ArgumentNullException.ThrowIfNull(message);
		Message = message;
	}
}
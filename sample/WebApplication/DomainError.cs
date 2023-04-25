// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace SampleWebApplication;

public class DomainError: LogicError
{
	public enum ErrorSeverity
	{
		Low,
		Medium,
		High
	}
	
	public int Code { get; }
	public ErrorSeverity Severity { get; }
	public DomainError(string message, int code, ErrorSeverity severity = ErrorSeverity.Low)
		: base(message)
	{
		Code = code;
		Severity = severity;
	}
}
using Bogoware.Monads;
// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Sample.Pipelines;

public static class CreateUserPipeline
{
    private const string DEMO_USER = "username@sample.com";
    // Models
    private record Username(string Value);
    private record User(Username Username, string FirstName, string LastName);
    public static void Run(string? username = null!)
    {
        username ??= DEMO_USER;
        Console.WriteLine($"Creating user '{username}'");
        var outcome = CreateUsername(username)
            .Ensure(u => LookupUser(u).IsNone, new LogicError("User already exists"))
            .Map(u => new User(u, "FirstName", "LastName"))
            .Bind(CreateUser)
            .IfSuccess(NotifyCreation)
            .Match(u => "User created.", e => $"The following error occurred: {e.Message}");

        // In a real example the final Match would return a specific model, for example, an IActionResult
        // or an IResult (Http) in case of AspNet Web Api.

        Console.WriteLine(outcome);
    }

	private static Result<User> CreateUser(User user)
	{
		// ReSharper disable once ConvertIfStatementToReturnStatement
		if (false) return Result.Failure<User>("Error creating user");
		return new(user);
	}
	private static void NotifyCreation(User user)
	{
		Console.WriteLine("  >>> User has been notified!");
	}
}
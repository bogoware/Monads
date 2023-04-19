namespace SampleWebApplication;

public static class OptionalExtensions
{
	public static IResult ToResult<T>(this Optional<T> optional) where T : class
		=> optional.Map(Results.Ok).GetValue(Results.NotFound());
}
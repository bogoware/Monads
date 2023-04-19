namespace SampleWebApplication;

public static class MaybeExtensions
{
	public static IResult ToResult<T>(this Maybe<T> maybe) where T : class
		=> maybe.Map(Results.Ok).GetValue(Results.NotFound());
}
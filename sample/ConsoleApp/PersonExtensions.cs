namespace Sample;

public static class PersonExtensions
{
	public static string GetFullName(this Person person)
		=> person
			.Surname
			.Map(surname => $"{person.Name} {surname}")
			.GetValue(() => person.Name);
}
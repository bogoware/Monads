using Bogoware.Monads;

namespace Sample;

public record Person(string Name, Maybe<string> Surname);
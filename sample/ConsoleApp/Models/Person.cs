using Bogoware.Monads;

namespace Sample.Models;

public record Person(string Name, Maybe<string> Surname);
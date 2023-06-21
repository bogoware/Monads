using Bogoware.Monads;

namespace Sample.Models;

public record Book(string Title, Maybe<Person> Author);
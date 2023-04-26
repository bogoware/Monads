using Bogoware.Monads;

namespace Sample;

public record Book(string Title, Maybe<Person> Author);
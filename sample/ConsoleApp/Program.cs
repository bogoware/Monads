using System.Diagnostics;
using Bogoware.Monads;

// ReSharper disable PossibleMultipleEnumeration

var bourbaki = new Person("Bourbaki", null);
var euler = new Person("Leonhard", "Euler");
var erdos = new Person("Pál", "Erdős");

var algebra = new Book("Algebra", bourbaki);
var elementsOfAlgebra = new Book("Elements of Algebra", euler);
var mathematicalTables = new Book("Mathematical Tables", null);

Console.WriteLine(bourbaki.GetFullName());
Console.WriteLine(erdos.GetFullName());
Console.WriteLine(elementsOfAlgebra.GetPrintableTitle());
Console.WriteLine(mathematicalTables.GetPrintableTitle());


IEnumerable<string> none =
	from b in bourbaki.Surname
	from e in euler.Surname
	select $"{b} {e}";

Debug.Assert(!none.Any());

IEnumerable<string> superMather =
	from b in erdos.Surname
	from e in euler.Surname
	select $"{b} {e}";

Debug.Assert(superMather.Count() == 1);

Console.WriteLine($"Super Mathematician: {superMather.First()}");


// Processing a list of Maybe<>s

var books = new List<Maybe<Book>> { algebra, elementsOfAlgebra, mathematicalTables, None<Book>() };

var validBooks = books.SelectValues(); // Retrieve values of Maybes that hold some value, None are discarded  

Console.WriteLine("=== VALID BOOKS");
foreach (var b in validBooks)
{
	Console.WriteLine(b);
}

var validAuthors = books
	.Bind(x => x.Author)
	.WhereNot(a => a.Name.StartsWith("Xulu"))
	.WhereNot(a => a.Surname.Satisfy(x => x.StartsWith("Fugu")))
	.SelectValues();

var bookDescriptions = books.Map(
		b => $"Title: {b.Title}, Author: {b.Author.Map( // Include the Author
				a => $"{a.Name}{a.Surname.Map(
						s => $" {s}") // Append the surname
					.GetValue("")}")  // or nothing
			.GetValue("Unknown")}")   // Unknown author 
	.SelectValues();


Console.WriteLine("=== VALID AUTHORS");
foreach (var a in validAuthors)
{
	Console.WriteLine(a);
}

Console.WriteLine("=== BOOK DESCRIPTIONS");
foreach (var _ in bookDescriptions)
{
	Console.WriteLine(_);
}
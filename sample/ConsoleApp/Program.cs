using System.Diagnostics;
// ReSharper disable PossibleMultipleEnumeration

var bourbaki = new Person("Bourbaki", null);
var euler = new Person("Leonhard", "Euler");
var erdos = new Person("Pál", "Erdős");

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
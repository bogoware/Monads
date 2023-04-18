using System.Diagnostics;
// ReSharper disable PossibleMultipleEnumeration

var bourbaki = new Mathematician("Bourbaki", null);
var euler = new Mathematician("Leonhard", "Euler");
var erdos = new Mathematician("Pál", "Erdős");

Console.WriteLine(bourbaki);
Console.WriteLine(euler);

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

Console.WriteLine(superMather.First());
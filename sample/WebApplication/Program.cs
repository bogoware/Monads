
// ReSharper disable ConvertClosureToMethodGroup

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/some", () => Some("Hello World!"));
app.MapGet("/none", () => None<string>());
app.MapGet("/notfound", () => Results.NotFound());

app.Run();
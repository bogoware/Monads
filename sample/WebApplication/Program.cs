// ReSharper disable ConvertClosureToMethodGroup

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/some", () => Some("Hello World!").ToResult());
app.MapGet("/none", () => None<string>().ToResult());

app.Run();
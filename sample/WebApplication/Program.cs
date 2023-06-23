// ReSharper disable ConvertClosureToMethodGroup

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/some", () => Maybe.Some("Hello World!").ToResult());
app.MapGet("/none", () => Maybe.None<string>().ToResult());

//app.MapGet("/exception", () => Try(() => { throw new NotImplementedException(); }).ToResult());

app.Run();
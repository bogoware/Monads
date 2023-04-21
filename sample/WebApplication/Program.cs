// ReSharper disable ConvertClosureToMethodGroup

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/some", () => Some("Hello World!").ToResult());
app.MapGet("/none", () => None<string>().ToResult());

//app.MapGet("/exception", () => Try(() => { throw new NotImplementedException(); }).ToResult());

app.Run();
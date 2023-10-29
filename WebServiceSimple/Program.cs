using DataLayer;
using WebServiceSimple.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IDataService, DataService>();

var app = builder.Build();

app.UseAuth();

//app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();

using DataLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IDataService, DataService>();

var app = builder.Build();

app.MapControllers();

app.Run();

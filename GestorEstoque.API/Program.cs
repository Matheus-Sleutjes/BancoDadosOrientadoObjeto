var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(cors => cors.AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

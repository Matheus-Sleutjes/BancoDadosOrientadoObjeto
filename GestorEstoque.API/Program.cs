using GestorEstoque.API.Configuration;
using GestorEstoque.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.RegisterConfig();

Utils.StringConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GestorEstoqueContext>(options =>
{
    options.UseNpgsql(Utils.StringConnection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(cors => cors.AllowAnyOrigin());

//var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
//var context = serviceScope.ServiceProvider.GetRequiredService<GestorEstoqueContext>();
//context.Database.Migrate();

app.UseAuthorization();

app.MapControllers();

app.Run();

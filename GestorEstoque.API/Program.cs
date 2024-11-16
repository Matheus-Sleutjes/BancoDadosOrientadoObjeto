using GestorEstoque.API.Configuration;
using GestorEstoque.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterConfig();

builder.Services.AddSwaggerGen();

Utils.StringConnection = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(cors => cors.AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

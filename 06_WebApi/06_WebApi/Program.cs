using _06_WebApi;
using Microsoft.EntityFrameworkCore;
using _06_WebApi.Controllers;
using _06_WebApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

builder.Services.AddScoped<IGeneric, GenericRepository>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

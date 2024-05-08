﻿using _06_WebApi;
using Microsoft.EntityFrameworkCore;
using _06_WebApi.Controllers;
using _06_WebApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // добавляем сервисы CORS
// Add services to the container.

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IGeneric, GenericRepository>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("https://localhost:7100").AllowAnyHeader().AllowAnyMethod());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


app.Run();
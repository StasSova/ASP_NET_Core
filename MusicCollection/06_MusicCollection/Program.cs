using _06_MusicCollection.Services.Language;
using Microsoft.EntityFrameworkCore;
using MusicCollection_BLL.Interfaces.Music;
using MusicCollection_BLL.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSpotifyContext(connection);

builder.Services.AddUserAuthentication();
builder.Services.AddPasswordHashing();
builder.Services.AddUnitOfWork();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddScoped<ILangRead, ReadLangServices>();



builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Длительность сеанса (тайм-аут завершения сеанса)
    options.Cookie.Name = "Session"; // Каждая сессия имеет свой идентификатор, который сохраняется в куках.

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

#if DEBUG
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");
#endif

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

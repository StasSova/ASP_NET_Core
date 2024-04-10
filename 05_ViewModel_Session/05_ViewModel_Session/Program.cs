using _05_ViewModel_Session.Models.DataBase;
using _05_ViewModel_Session.Services.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GuestBookContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Длительность сеанса (тайм-аут завершения сеанса)
    options.Cookie.Name = "Session"; // Каждая сессия имеет свой идентификатор, который сохраняется в куках.

});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseSession();   // Добавляем middleware-компонент для работы с сессиями

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

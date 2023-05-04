using GameStoreApp.Data;
using GameStoreApp.Data.Cart;
using GameStoreApp.Data.Services;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DbContext configuration
builder.Services.AddDbContext<GameStoreAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Services configuration
builder.Services.AddScoped<IVoiceActorService, VoiceActorService>();
builder.Services.AddScoped<IGameDeveloperService, GameDeveloperService>();
builder.Services.AddScoped<IGamePublisherService, GamePublisherService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IGameRatingService, GameRatingService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Authentication and authorisation
builder.Services.AddIdentity<GameStoreUser, IdentityRole>().AddEntityFrameworkStores<GameStoreAppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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
app.UseSession();

//Authentication and Authorisation
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Game}/{action=Index}/{id?}");

//seed database
GameAppDbInitialiser.Seed(app);
GameAppDbInitialiser.SeedUsersAndRolesAsync(app).Wait();

app.Run();

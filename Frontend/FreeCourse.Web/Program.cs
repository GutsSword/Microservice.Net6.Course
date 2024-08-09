using FluentValidation.AspNetCore;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Extension;
using FreeCourse.Web.Handler;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Concrete;
using FreeCourse.Web.Services.Interfaces;
using FreeCourse.Web.Validators.Catolog;
using FreeCourse.Web.Validators.Discount;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
 
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApi>(builder.Configuration.GetSection("ServiceApi"));

var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddHttpServices(builder.Configuration);  // Extension Metod.

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<PhotoHelper>();

builder.Services.AddFluentValidation().AddFluentValidation(x => 
x.RegisterValidatorsFromAssemblyContaining<CreateCourseViewModelValidator>());


builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Auth/SignIn";
        opt.ExpireTimeSpan = TimeSpan.FromDays(60);
        opt.SlidingExpiration = true;
        opt.Cookie.Name = "webcookie";
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

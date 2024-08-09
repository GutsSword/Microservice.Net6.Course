using FreeCourse.BasketService.Consumer;
using FreeCourse.BasketService.Redis;
using FreeCourse.BasketService.Services;
using FreeCourse.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// AuthenticationFilter için gerekli.
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");  // Sub'ý maplemekten kurtarýr.

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Audience = "resource_basket";
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddHttpContextAccessor();   // ********
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;  // appsettingsteki datalarý aktararak deðer döndürür.
    var redis = new RedisService(redisSettings.Host,redisSettings.Port); // Yeni bir nesne örneði oluþtur.
    redis.Connect(); // Baðlantýyý kur.

    return redis;
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CourseNameChangedBasketConsumer>();
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("course-NameChanged-basket-event", e =>
        {
            e.ConfigureConsumer<CourseNameChangedBasketConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

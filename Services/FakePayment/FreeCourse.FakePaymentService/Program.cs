
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicty = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Audience = "resource_fake_payment";
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.RequireHttpsMetadata = false;
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicty));
});


//builder.Services.AddMassTransit(x =>
//{
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {
//        cfg.Host(builder.Configuration["RabbitMqUrl"], "/", hostConfig =>
//        {
//            hostConfig.Username("guest");
//            hostConfig.Password("guest");
//        });
//    }));
//});

builder.Services.AddMassTransit(x =>
{
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});

builder.Services.AddMassTransitHostedService();

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
var bus = app.Services.GetRequiredService<IBusControl>();
bus.Start();
app.Run();

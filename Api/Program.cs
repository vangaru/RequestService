using Microsoft.EntityFrameworkCore;
using RequestService.Api.Data;
using RequestService.Api.Repositories;
using RequestService.Api.Services;

const string defaultConnectionKey = "DefaultConnection";

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.AddTransient<IRouteService, RouteService>();
builder.Services.AddTransient<IRequestService, RequestService.Api.Services.RequestService>();
builder.Services.AddDbContext<RequestsContext>(options 
    => options.UseNpgsql(configuration.GetConnectionString(defaultConnectionKey)));
builder.Services.AddTransient<IRequestsRepository, RequestsRepository>();
builder.Services.AddTransient<IIntervalService, IntervalService>();

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
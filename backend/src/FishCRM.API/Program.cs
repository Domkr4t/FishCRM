using FishCRM.Application.Implementations;
using FishCRM.Application.Interfaces;
using FishCRM.Infrastructure;
using FishCRM.Infrastructure.Interfaces;
using FishCRM.Infrastructure.Repositories;
using FIshCRM.Domain.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBaseRepository<FishBase>, FishBaseRepository>();
builder.Services.AddScoped<IBaseRepository<Fish>, FishRepository>();
builder.Services.AddScoped<IFishCRMService, FishCRMService>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

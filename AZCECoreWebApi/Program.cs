using AZCECoreWebApi.Cache;
using AZCECoreWebApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
Microsoft.Extensions.Configuration.ConfigurationManager Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

ConfigureServices(builder.Services);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    //sql
    var dbCon = Configuration.GetConnectionString("DBConnection");
    services.AddDbContext<AppDbContext>(opt =>
        //opt.UseInMemoryDatabase("TodoList"));
        opt.UseSqlServer(dbCon));

    services.AddMemoryCache();

    services.AddScoped<ICacheProvider, CacheProvider>();
}
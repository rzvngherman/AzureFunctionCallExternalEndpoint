using BlazorServerApp1.Data.WeatherForecast;
using BlazorServerApp1.Data.WeatherForecastServiceExternal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//add 'WeatherForecastService'
//builder.Services.AddSingleton<WeatherForecastService>();
//builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<IWeatherForecastServiceExternal, WeatherForecastServiceExternal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

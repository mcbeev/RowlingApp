using Kontent.Ai.Delivery;
using Kontent.Ai.Delivery.Abstractions;
using Kontent.Ai.Delivery.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using RowlingApp.Data;
using RowlingApp.Models.Generated;
using RowlingApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Application services and configuration setup
var Configuration = builder.Configuration;

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddFeatureManagement();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ITypeProvider, CustomTypeProvider>();
builder.Services.AddHttpClient<IDeliveryHttpClient, DeliveryHttpClient>();
builder.Services.AddDeliveryClient(Configuration);
//builder.Services.AddHttpClient<KontentManagementBetaService>();
builder.Services.AddSingleton<KontentManagementService>();
builder.Services.AddSingleton<TeamService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAzureAppConfiguration();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
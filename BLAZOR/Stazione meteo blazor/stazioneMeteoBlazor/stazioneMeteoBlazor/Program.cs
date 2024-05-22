using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using stazioneMeteoBlazor.Dati.GrandezzaFisica;
using stazioneMeteoBlazor.Dati.Rilevazioni;
using stazioneMeteoBlazor.Dati.Sensori;
using stazioneMeteoBlazor.Dati.SensoriInstallati;
using stazioneMeteoBlazor.Dati.Stazioni;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//Servizio SQL
var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:db_meteo_Docker");
builder.Services.AddScoped<RilevazioniQuery>(sp => new RilevazioniQuery(connectionString));
builder.Services.AddScoped<SensoriQuery>(sp => new SensoriQuery(connectionString));
builder.Services.AddScoped<GrandezzaFisicaQuery>(sp => new GrandezzaFisicaQuery(connectionString));
builder.Services.AddScoped<SensoriInstallatiQuery>(sp => new SensoriInstallatiQuery(connectionString));
builder.Services.AddScoped<StazioniQuery>(sp => new StazioniQuery(connectionString));

builder.Services.AddBlazorBootstrap();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

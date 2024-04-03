using BioTranan.Blazor.Components;
using BioTranan.Core.Database;
using BioTranan.Core.Services;
using BioTranan.Core.Interfaces;
using BioTranan.OmdbApi;
using BioTranan.TmdbApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

Infrastructure.AddDbContext(builder.Configuration, builder.Services);
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();
builder.Services.AddScoped<IScreeningService, ScreeningService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<TmdbApiClient>();
builder.Services.AddScoped<OmdbApiClient>(provider =>
{
    string apiKey = "d1651ad8";
    return new OmdbApiClient(apiKey);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
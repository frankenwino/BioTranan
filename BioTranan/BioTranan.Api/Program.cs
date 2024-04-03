using BioTranan.Core.Database;
using BioTranan.Core.Services;
using BioTranan.OmdbApi;
using BioTranan.TmdbApi;
using BioTranan.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
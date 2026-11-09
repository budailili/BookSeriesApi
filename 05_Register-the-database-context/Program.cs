using Microsoft.EntityFrameworkCore;
using BookSeriesApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BookContext>(opt =>
    opt.UseInMemoryDatabase("BookSeriesList"));

var app = builder.Build();

// Kezdőadatok (Seed) betöltése az In-Memory adatbázisba
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookContext>();
    if (!db.BookItems.Any())
    {
        db.BookItems.AddRange(
            new BookItem
            {
                Title = "The Deal - Az üzlet",
                Author = "Elle Kennedy",
                SeriesName = "Off-Campus #1",
                MainCouple = "Hannah Wells & Garrett Graham",
                IsRead = true,
                SpiceRating = 4
            },
            new BookItem
            {
                Title = "The Mistake - A hiba",
                Author = "Elle Kennedy",
                SeriesName = "Off-Campus #2",
                MainCouple = "Grace Ivers & John Logan",
                IsRead = true,
                SpiceRating = 4
            },
            new BookItem
            {
                Title = "Twisted Love",
                Author = "Ana Huang",
                SeriesName = "Twisted #1",
                MainCouple = "Ava Chen & Alex Volkov",
                IsRead = false,
                SpiceRating = 5
            }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
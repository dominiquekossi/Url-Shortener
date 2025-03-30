using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Url_Shortener.Entites;
using Url_Shortener.Extensions;
using Url_Shortener.Infrastructure;
using Url_Shortener.Models;
using Url_Shortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnetion"));
        });
builder.Services.AddControllers();
builder.Services.AddScoped<UrlShorteningService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "URL Shortener API",
        Version = "v1",
        Description = "API para encurtamento de URLs usando Minimal API no .NET 8."
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
app.MapPost("api/shortener", async (
    ShortenerUrlRequest request,
    UrlShorteningService urlShorteningService,
    ApplicationDbContext applicationDbContext,
    HttpContext httpContext
) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("The specified URL is invalid");
    }

    string code;
    do
    {
        code = await urlShorteningService.GenerateUniqueCode();
    }
    while (await applicationDbContext.shortenerUrls.AnyAsync(x => x.code == code));  

    var shortenedUrl = new ShortenerUrl
    {
        Id = Guid.NewGuid(),
        LongUrl = request.Url,
        ShortUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/api/{code}",
        code = code,  
        CreateAt = DateTime.Now,
    };

    applicationDbContext.shortenerUrls.Add(shortenedUrl);
    await applicationDbContext.SaveChangesAsync();

    return Results.Ok(shortenedUrl.ShortUrl);
});

app.MapGet("/api/{code}", async (
    string code,
    ApplicationDbContext applicationDbContext
) =>
{
    var shortenedUrl = await applicationDbContext.shortenerUrls
        .FirstOrDefaultAsync(x =>x.code == code);

    if (shortenedUrl is null)
    {
        return Results.NotFound("URL not found");
    }

    return Results.Redirect(shortenedUrl.LongUrl);
})
.WithName("RedirectUrl");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

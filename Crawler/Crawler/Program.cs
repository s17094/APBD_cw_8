using Crawler.Context;
using Crawler.Filters;
using Crawler.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MainDbContext>(option =>
{
    option
        .LogTo(Console.WriteLine)
        .UseSqlServer()
        .UseSqlServer(builder.Configuration.GetConnectionString("ProductionDb"));
});

builder.Services.AddScoped<IDoctorsService, DoctorsService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

builder.Services
    .AddControllers(option =>
    {
        option.Filters.Add<RestExceptionFilter>();
    })
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

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
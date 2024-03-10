using Assignment.Core;
using Assignment.Core.Models;
using Assignment.Core.Services;
using Assignment.Core.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "Assignment project by Panos Mazarakis."
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Assignment.Core.xml"));
});

// Register your service here
builder.Services.AddScoped<ISecondLargestService, SecondLargestService>();
builder.Services.AddScoped<IRestCountriesApiClient, RestCountriesApiClient>();
builder.Services.AddScoped<ICountriesService, CountriesService>();

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

// Configure the database connection
builder.Services.AddDbContext<AssignmentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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

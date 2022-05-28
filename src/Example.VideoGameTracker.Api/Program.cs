using System.Text.Json;
using System.Text.Json.Serialization;
using Example.VideoGameTracker.Api;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddSingleton<IConfigureOptions<RawgOptions>, ConfigureRawgOptions>();
builder.Services.AddVideoGameDatabase();
builder.Services.AddUserDatabase();
builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
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

app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();

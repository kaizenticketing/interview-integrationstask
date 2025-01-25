using interview_integrationstask.Models; 
using interview_integrationstask.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Football API options 
builder.Services.Configure<FootballApiOptions>(
    builder.Configuration.GetSection(FootballApiOptions.ConfigSection));

// Register HttpClient with default configuration 
builder.Services.AddHttpClient("FootballApi", (serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<FootballApiOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
    client.DefaultRequestHeaders.Add("X-Auth-Token", options.ApiKey);
});

// Register Football API service 
builder.Services.AddScoped<IFootballApiService, FootballApiService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


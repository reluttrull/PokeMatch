using DeckApi;
using DeckApi.Extensions;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

//string root = Directory.GetCurrentDirectory();
string solutionEnvironmentPath = $".env.{builder.Environment.EnvironmentName}";
string solutionDefaultPath = ".env";
if (builder.Environment.IsDevelopment())
{
    if (File.Exists(solutionEnvironmentPath))
    {
        Env.Load(solutionEnvironmentPath);
    }
    else
    {
        Env.Load(solutionDefaultPath);
    }
}

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddCache();
builder.Services.AddHttpClient<ICardApiClient, CardApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.tcgdex.net/v2/en/cards/");
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

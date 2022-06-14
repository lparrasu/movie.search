using Movie.Search.Core.Abstractions.ServiceClient;
using Movie.Search.Core.Entities.ConfigOptions;
using Movie.Search.Core.Extensions;
using Movie.Search.Infrastructure.Extensions;
using Movie.Search.Infrastructure.Services.Client;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions<TMDBOptions>().Bind(configuration.GetSection(nameof(TMDBOptions)))
                .ValidateDataAnnotations();
builder.Services.AddCoreLayer(configuration);
builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddSingleton<ITMDBServiceClient, TMDBServiceClient>();

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
public partial class Program { };
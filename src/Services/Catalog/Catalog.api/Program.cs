using BuildingBlocks.Behavoirs;
using BuildingBlocks.Exceptions.Handler;
using Catalog.api.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Add services here

#region Regiser services
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    //add validation behavoir pipelie
    config.AddOpenBehavior(typeof(ValidationBehavoir<,>));
    //add logging behavoir pipelie
    config.AddOpenBehavior(typeof(LoggingBehavoir<,>));
});

//register maretn lib
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

//register seeding of data
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

//register fluentValidation

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();   // register custom exception Handlker

builder.Services.AddHealthChecks()   //perform health check for backend service
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);  // perform health check for pg database

#endregion

var app = builder.Build();

// configure HTTP Request PipeLine.
app.MapCarter();

//add exception handler to request pipeline
app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

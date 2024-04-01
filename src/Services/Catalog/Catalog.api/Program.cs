using BuildingBlocks.Behavoirs;
using BuildingBlocks.Exceptions.Handler;
using Catalog.api.Data;
using Microsoft.AspNetCore.Diagnostics;
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
    opt.Connection(builder.Configuration.GetConnectionString("connectionString")!);
}).UseLightweightSessions();

//register seeding of data
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

//register fluentValidation

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();   // register custom exception Handlker

#endregion

var app = builder.Build();

// configure HTTP Request PipeLine.
app.MapCarter();

//add exception handler to request pipeline
app.UseExceptionHandler(opt => { });

app.Run();

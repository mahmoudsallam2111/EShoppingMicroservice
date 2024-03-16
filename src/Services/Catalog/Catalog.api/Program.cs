var builder = WebApplication.CreateBuilder(args);

//Add services here

#region Regiser services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
#endregion

var app = builder.Build();

// configure HTTP Request PipeLine.
app.MapCarter();

app.Run();

using YarpApiGateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseServices();

app.Run();

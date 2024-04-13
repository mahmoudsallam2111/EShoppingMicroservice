using Ordering.API;
using Ordering.Application;
using Ordering.Infrastrucure;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

builder.Services.
     AddApplicationService()
    .AddInfrastractureservices(builder.Configuration)
    .AddApiServices();

#endregion


var app = builder.Build();

app.Run();

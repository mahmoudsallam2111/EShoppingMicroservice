using Ordering.API;
using Ordering.Application;
using Ordering.Infrastrucure;
using Ordering.Infrastrucure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

builder.Services.
     AddApplicationService()
    .AddInfrastractureservices(builder.Configuration)
    .AddApiServices();

#endregion

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
   await app.InitialiseDataBase();
}

app.Run();

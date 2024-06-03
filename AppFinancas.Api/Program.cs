using AppFinancas.Api;
using AppFinancas.Api.Common.Api;
using AppFinancas.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseCors(ApiConfiguration.SharedPolicyName);
app.MapEndpoints();

app.Run();
 
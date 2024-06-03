using AppFinancas.Api.Data;
using AppFinancas.Api.Handlers;
using AppFinancas.Shared;
using AppFinancas.Shared.Handlers;
using Microsoft.EntityFrameworkCore;

namespace AppFinancas.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ?? string.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }

    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(ApiConfiguration.ConnectionString));  
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(opt => opt.AddPolicy(ApiConfiguration.SharedPolicyName,
                                policy => policy.WithOrigins
                                ([Configuration.BackendUrl, Configuration.FrontendUrl])
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}

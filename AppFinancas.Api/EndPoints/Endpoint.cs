using AppFinancas.Api.Common.Api;
using AppFinancas.Api.EndPoints.Categories;
using AppFinancas.Api.EndPoints.Transactions;

namespace AppFinancas.Api.EndPoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endPoints = app.MapGroup("");

        endPoints.MapGroup("/")
                 .WithTags("Health Check")
                 .MapGet("/", () => new { message = "OK" });

        endPoints.MapGroup("v1/categories")
                 .WithTags("Categories")
                 .MapEndpoint<CreateCategoryEndpoint>()
                 .MapEndpoint<UpdateCategoryEndpoint>()
                 .MapEndpoint<DeleteCategoryEndpoint>()
                 .MapEndpoint<GetCategoryByIdEndpoint>()
                 .MapEndpoint<GetAllCategoriesEndpoint>();

        endPoints.MapGroup("v1/transactions")
                 .WithTags("Transactions")
                 .MapEndpoint<CreateTransactionEndpoint>()
                 .MapEndpoint<UpdateTransactionEndpoint>()
                 .MapEndpoint<DeleteTransactionEndpoint>()
                 .MapEndpoint<GetTransactionByIdEndpoint>()
                 .MapEndpoint<GetTransactionByPeriodEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}

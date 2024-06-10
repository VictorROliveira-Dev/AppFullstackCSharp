using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Categories;

public class CreateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync).WithName("Categories: Create")
                                     .WithSummary("Cria uma nova categoria")
                                     .WithDescription("Cria uma nova categoria")
                                     .WithOrder(1)
                                     .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);
        return response.IsSuccess ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response) : TypedResults.BadRequest(response);
    }
}

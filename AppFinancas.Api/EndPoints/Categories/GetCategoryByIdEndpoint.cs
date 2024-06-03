using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Categories;

public class GetCategoryByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync).WithName("Categoires: Get By Id")
                                        .WithSummary("Recupera uma categoria")
                                        .WithDescription("Recupera uma categoria")
                                        .WithOrder(4)
                                        .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new GetCategoryByIdRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}

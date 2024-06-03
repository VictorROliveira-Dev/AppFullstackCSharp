using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Categories;

public class DeleteCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync).WithName("Categories: Delete")
                                           .WithSummary("Remove uma categoria")
                                           .WithDescription("Remove uma categoria")
                                           .WithOrder(3)
                                           .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, long id)
    {
        var request = new DeleteCategoryRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}

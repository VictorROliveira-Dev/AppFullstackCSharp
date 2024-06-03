using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Categories;

public class UpdateCategoryEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync).WithName("Categories: Update")
                                        .WithSummary("Atualiza uma categoria")
                                        .WithDescription("Atualiza uma categoria")
                                        .WithOrder(2)
                                        .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var result = await handler.UpdateAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);    
    }
}

using AppFinancas.Api.Common.Api;
using AppFinancas.Shared;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppFinancas.Api.EndPoints.Categories;

public class GetAllCategoriesEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync).WithName("Categories: Get All")
                                    .WithSummary("Recupera todas as categorias")
                                    .WithDescription("Recupera todas as categorias")
                                    .WithOrder(5)
                                    .Produces<PagedResponse<List<Category>?>>();
    }

    private static async Task<IResult> HandleAsync(ICategoryHandler handler, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}

using AppFinancas.Api.Common.Api;
using AppFinancas.Shared;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Transactions;
using AppFinancas.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AppFinancas.Api.EndPoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync).WithName("Transaction: Get By Period")
                                    .WithSummary("Recupera transações")
                                    .WithDescription("Recupera transações dentro de um período")
                                    .WithOrder(5)
                                    .Produces<PagedResponse<List<Transaction>?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null, [FromQuery] int pageNumber = Configuration.DefaultPageNumber, [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetTransactionsByPeriodRequest
        {
            UserId = ApiConfiguration.UserId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate,
        };

        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);    
    }
}

using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Transactions;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Transactions;

public class GetTransactionByIdEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync).WithName("Transaction: Get By Id")
                                        .WithSummary("Recupera uma transação")
                                        .WithDescription("Recupera uma transação")
                                        .WithOrder(4)
                                        .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);    
    }
}

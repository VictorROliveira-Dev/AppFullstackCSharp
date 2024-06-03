using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Transactions;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Transactions;

public class DeleteTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync).WithName("Transaction: Delete")
                                           .WithSummary("Remove uma transação")
                                           .WithDescription("Remove uma transação")
                                           .WithOrder(3)
                                           .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
    {
        var request = new DeleteTransactionRequest
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);    
    }
}

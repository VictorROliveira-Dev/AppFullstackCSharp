using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Transactions;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Transactions;

public class UpdateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync).WithName("Transaction: Update")
                                        .WithSummary("Atualiza uma transação")
                                        .WithDescription("Atualiza uma transação através de um ID")
                                        .WithOrder(2)
                                        .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.UserId = ApiConfiguration.UserId;
        request.Id = id;

        var result = await handler.UpdateAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }
}

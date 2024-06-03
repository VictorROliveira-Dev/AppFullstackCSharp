using AppFinancas.Api.Common.Api;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Transactions;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Api.EndPoints.Transactions;

public class CreateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync).WithName("Transactions: Create")
                                     .WithSummary("Cria uma nova transação")
                                     .WithDescription("Cria uma nova transação")
                                     .WithOrder(1)
                                     .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
    {
        request.UserId = ApiConfiguration.UserId;

        var result = await handler.CreateAsync(request);
        return result.IsSuccess ? TypedResults.Created($"/{result.Data?.Id}") : TypedResults.BadRequest(result);
    }
}

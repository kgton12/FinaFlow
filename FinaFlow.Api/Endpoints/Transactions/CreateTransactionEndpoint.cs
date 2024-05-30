using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Responses;
using System.Transactions;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandlerAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transação")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"v1/transactions/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result.Data);
        }
    }
}

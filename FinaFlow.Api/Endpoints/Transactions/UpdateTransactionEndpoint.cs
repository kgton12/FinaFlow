using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Responses;
using System.Transactions;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandlerAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(6)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync
            (ITransactionHandler handler,
            UpdateTransactionRequest request,
            long id)
        {
            request.Id = id;
            request.UserId = ApiConfiguration.UserId;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);

        }
    }
}

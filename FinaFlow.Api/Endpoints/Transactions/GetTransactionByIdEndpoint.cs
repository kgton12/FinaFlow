using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Responses;
using System.Transactions;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandlerAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Obtém uma transação pelo ID")
            .WithDescription("Obtém uma transação pelo ID")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandler handler, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                Id = id,
                UserId = ApiConfiguration.UserId
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

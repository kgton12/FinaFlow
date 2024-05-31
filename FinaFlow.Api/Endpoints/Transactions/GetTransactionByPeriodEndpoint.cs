using FinaFlow.Api.Common.Api;
using FinaFlow.Core;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Requests.Transactions;
using FinaFlow.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace FinaFlow.Api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandlerAsync)
            .WithName("Transactions: Get By Period")
            .WithSummary("Obtém uma transação pelo período")
            .WithDescription("Obtém uma transação pelo período")
            .WithOrder(5)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync(
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionsByPerioadRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                PageSize = pageSize,
                UserId = ApiConfiguration.UserId
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

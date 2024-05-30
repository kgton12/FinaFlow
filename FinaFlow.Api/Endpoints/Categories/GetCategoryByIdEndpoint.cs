using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Responses;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Recupera uma categoria pelo Id")
            .WithDescription("Recupera uma categoria cadastrada no sistema pelo Id")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandlerAsync(
            ICategoryHandler handler,
            long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.NotFound(result);
        }
    }
}

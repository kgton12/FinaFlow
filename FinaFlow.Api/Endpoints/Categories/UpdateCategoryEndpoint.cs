using FinaFlow.Api.Common.Api;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Responses;

namespace FinaFlow.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandlerAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria cadastrada no sistema")
            .WithOrder(5)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandlerAsync(
            ICategoryHandler handler,
            long id,
            UpdateCategoryRequest request)
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

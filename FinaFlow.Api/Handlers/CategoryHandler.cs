using FinaFlow.Api.Data;
using FinaFlow.Core.Handlers;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using FinaFlow.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId
            };

            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, 201, "Categoria criado com sucesso.");
            }
            catch (Exception)
            {
                return new Response<Category?>(null, 500, "Não foi possivel criar a Categoria");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada.");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, 200, "Categoria deletada com sucesso.");
            }
            catch (Exception)
            {
                return new Response<Category?>(null, 500, "Não foi possivel excluir categoria");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context.Categories.AsNoTracking().Where(c => c.UserId == request.UserId).OrderBy(c => c.Title);

                var categories = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {
                return new PagedResponse<List<Category>?>(null, 500, "Não foi possivel recuperar as Categorias");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return category is null
                    ? new Response<Category?>(null, 404, "Categoria não encontrada.")
                    : new Response<Category?>(category, 200, "Categoria encontrada com sucesso.");
            }
            catch (Exception)
            {
                return new Response<Category?>(null, 500, "Não foi possivel recuperar a categoria.");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);
                if (category is null)
                {
                    return new Response<Category?>(null, 404, "Categoria não encontrada.");
                }

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, 200, "Categoria atualizada com sucesso.");
            }
            catch (Exception)
            {

                return new Response<Category?>(null, 500, "Não foi possivel atualizar a Categoria.");
            }
        }
    }
}

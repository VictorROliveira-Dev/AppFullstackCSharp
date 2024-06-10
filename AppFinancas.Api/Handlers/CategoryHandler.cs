using AppFinancas.Api.Data;
using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace AppFinancas.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        await Task.Delay(3000);
        var category = new Category
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
        };

        try
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria criada com sucesso.");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possível criar uma categoria.");
        }
    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(cat => cat.Id == request.Id && cat.UserId == request.UserId);

            if (category == null)
            {
                return new Response<Category?>(null, 404, "Categoria não encontrada.");
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria removida com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Categoria não removida.");
        }
    }

    //Paginação:
    public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var query = context.Categories.AsNoTracking()
                                      .Where(cat => cat.UserId == request.UserId)
                                      .OrderBy(cat => cat.Title);

            var categories = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Category>?>(null, 404, "Não foi possível retornar as categorias.");
        }
    }

    public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try
        {
            // Utilizando o "asNoTracking" para que não seja consultado muitos
            // campos no banco, apenas o que for pedido, tornando a aplicação mais 
            // eficiente.
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(cat => cat.Id == request.Id && cat.UserId == request.UserId);

            return category is null ? new Response<Category?>(null, 404, "Categoria não encontrada.") : new Response<Category?>(category);
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não será possível retornar a categoria.");
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        await Task.Delay(3000);
        var category = await context.Categories.FirstOrDefaultAsync(cat => cat.Id == request.Id && cat.UserId == request.UserId);

        if (category == null)
        {
            return new Response<Category?>(null, 404, "Categoria não encontrada.");
        }

        category.Title = request.Title;
        category.Description = request.Description;

        context.Categories.Update(category);
        await context.SaveChangesAsync();

        return new Response<Category?>(category, 201, "Categoria atualizada com sucesso");
    }
}

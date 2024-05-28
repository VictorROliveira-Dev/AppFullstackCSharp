using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using AppFinancas.Shared.Responses;

namespace AppFinancas.Shared.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
    Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request);
}

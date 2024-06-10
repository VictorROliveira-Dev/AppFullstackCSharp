using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AppFinancas.Web.Pages.Categories;

public partial class UpdateCategoryPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public Category Category { get; set; } = new();

    [Parameter]
    public long CategoryId { get; set; }

    #endregion

    #region Services 

    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    #endregion

    #region Overrides 

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetCategoryByIdRequest
            {
                Id = CategoryId,
            };

            var result = await Handler.GetByIdAsync(request);

            if (result.IsSuccess)
            {
                Category = result.Data ?? new Category();
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods 

    public async Task OnSaveAsync()
    {
        IsBusy = true;

        try
        {
            var request = new UpdateCategoryRequest
            {
                Id = Category.Id,
                Title = Category.Title,
                Description = Category.Description ?? string.Empty
            };

            var result = await Handler.UpdateAsync(request);

            if (result.IsSuccess)
            {
                Snackbar.Add("Categoria atualizada com sucesso.👍🏼", Severity.Success);
                Navigation.NavigateTo("/categorias");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}

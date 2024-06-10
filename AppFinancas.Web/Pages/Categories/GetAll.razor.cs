using AppFinancas.Shared.Handlers;
using AppFinancas.Shared.Models;
using AppFinancas.Shared.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AppFinancas.Web.Pages.Categories;

public partial class GetAllCategoriesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = [];

    public EventCallback<DataGridRowClickEventArgs<Category>> RowClickCallback =>
        EventCallback.Factory.Create<DataGridRowClickEventArgs<Category>>(this, OnRowClick);

    #endregion

    #region Services 

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
            {
                Categories = result.Data ?? [];
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

    public async void OnDeleteButtonAsync(long id, string title)
    {
        var result = await Dialog.ShowMessageBox("Remoção", $"Deseja remover a categoria: {title} ?", yesText: "Remover", cancelText: "Cancelar");

        if (result is true)
        {
            await OnDeleteAsync(id, title);
        }
        //Atualiza a tela
        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest
            {
                Id = id,
            };

            await Handler.DeleteAsync(request);
            Categories.RemoveAll(cat => cat.Id == id);
            Snackbar.Add($"Categoria: ({title}) removida.", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public async void OnRowClick(DataGridRowClickEventArgs<Category> args)
    {
        var parameters = new DialogParameters
        {
            { "Title", args.Item.Title },
            { "Description", args.Item.Description }
        };

        var dialog = Dialog.Show<CategoryDialog>("Detalhes da Categoria:", parameters);
        await dialog.Result;
    }

    #endregion
}

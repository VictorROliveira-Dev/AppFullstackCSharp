using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AppFinancas.Web.Pages.Categories
{
    public partial class CategoryDialogBase : ComponentBase
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;

        [Parameter] public string Title { get; set; } = string.Empty;
        [Parameter] public string Description { get; set; } = string.Empty;

        protected void Close()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}

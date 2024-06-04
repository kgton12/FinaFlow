﻿using FinaFlow.Core.Handlers;
using FinaFlow.Core.Models;
using FinaFlow.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinaFlow.Web.Pages.Categories
{
    public partial class GetAllCategoriesPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<Category> Categories { get; set; } = [];

        #endregion

        #region

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
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
            catch (Exception)
            {
                Snackbar.Add("An error occurred while loading categories.", Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await Dialog.ShowMessageBox("ATENÇÃO",
                $"Ao proseguir a categoria {title} será removida. Desseja continuar?",
                yesText: "Excluir",
                cancelText: "Cancelar");
            if(result is true)
            {
                await OnDeleteAsync(id, title);
            }
            StateHasChanged();
        }
        public async Task OnDeleteAsync(long id, string title)
        {
            try
            {
                var request = new DeleteCategoryRequest { Id = id };
                await Handler.DeleteAsync(request);

                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} removida com sucesso.", Severity.Info);
            }
            catch (Exception ex)
            {

                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
    }
}

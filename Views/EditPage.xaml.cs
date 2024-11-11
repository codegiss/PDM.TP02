using TarefasApp.Models;
using TarefasApp.ViewModels;

namespace TarefasApp.Views;

public partial class EditPage : ContentPage
{
    public Tarefa TarefaSelecionada { get; set; }
    public EditPage(Tarefa tarefaSelecionada)
	{
		InitializeComponent();
        TarefaSelecionada = tarefaSelecionada;

        if (TarefaSelecionada == null)
        {
            DisplayAlert("Erro", "TarefaSelecionada é nula na EditPage", "OK");
        }

        var viewModel = new EditPageViewModel
        {
            TarefaSelecionada = tarefaSelecionada
        };

        BindingContext = new EditPageViewModel(tarefaSelecionada);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
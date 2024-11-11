using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Models;
using TarefasApp.ViewModels;

namespace TarefasApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailTaskPage : ContentPage
	{
        private DetailsPageViewModel ViewModel { get; set; }
        public Tarefa TarefaSelecionada { get; set; }
        public DetailTaskPage (Tarefa tarefaSelecionada)
		{
			InitializeComponent();
            TarefaSelecionada = tarefaSelecionada;

            MessagingCenter.Subscribe<DetailsPageViewModel, Tarefa>(this, "NavigateToEdit", (sender, tarefa) =>
            {
                Navigation.PushAsync(new EditPage(tarefa));
            });

            ViewModel = new DetailsPageViewModel();
            ViewModel.TarefaSelecionada = tarefaSelecionada;

            BindingContext = ViewModel;

            MessagingCenter.Subscribe<DetailsPageViewModel, Tarefa>(this, "DeleteTask", (sender, tarefa) =>
            {
                //DisplayAlert("aviso", $"Tarefa \"{tarefaSelecionada.Titulo}\" apagada", "ok");
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<EditPageViewModel, Tarefa>(this, "EditTask", (sender, tarefaAtualizada) =>
            {
                Console.WriteLine($"Tarefa recebida: {tarefaAtualizada.Titulo}");
                if (BindingContext is Tarefa tarefaAtual)
                {
                    if (tarefaAtual.Id == tarefaAtualizada.Id)
                    {
                        BindingContext = tarefaAtualizada;

                        OnPropertyChanged(nameof(TarefaSelecionada));
                    }
                }
                else
                {
                    Console.WriteLine("O BindingContext não é uma instância de Tarefa.");
                }
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<EditPageViewModel, Tarefa>(this, "EditTask");
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            // Envia a mensagem para navegar até a EditPage com a TarefaSelecionada
            MessagingCenter.Send(this, "NavigateToEdit", TarefaSelecionada);

            // Navega para a EditPage
            await Navigation.PushAsync(new EditPage(TarefaSelecionada));
        }
    }
}
using TarefasApp.Models;
using TarefasApp.ViewModels;

namespace TarefasApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new MainPageViewModel();
        }

        public async void OnTaskSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Tarefa tarefaSelecionada)
            {
                await Navigation.PushAsync(new DetailTaskPage(tarefaSelecionada));
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        private async void OnTaskAdd(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTaskPage());
            //((CollectionView)sender).SelectedItem = null;
        }
    }
}

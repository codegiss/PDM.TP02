using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TarefasApp.Models;
using TarefasApp.Views;

namespace TarefasApp.ViewModels
{
    internal class DetailsPageViewModel : BindableObject
    {
        public ICommand EditCommand { get; }
        private Tarefa _tarefaSelecionada;

        public Tarefa TarefaSelecionada
        {
            get => _tarefaSelecionada;
            set
            {
                _tarefaSelecionada = value;
                if (_tarefaSelecionada != null)
                {
                    Titulo = _tarefaSelecionada.Titulo;
                    Descricao = _tarefaSelecionada.Descricao;
                    Prioridade = _tarefaSelecionada.Prioridade;
                    DataCriacao = _tarefaSelecionada.DataCriacao;
                }
                OnPropertyChanged();
            }
        }
        private string _titulo;
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }

        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                OnPropertyChanged();
            }
        }

        private int _prioridade;
        public int Prioridade
        {
            get => _prioridade;
            set
            {
                _prioridade = value;
                OnPropertyChanged();
            }
        }

        private DateTime _dataCriacao;
        public DateTime DataCriacao
        {
            get => _dataCriacao;
            set
            {
                _dataCriacao = value;
                OnPropertyChanged();
            }
        }

        public DetailsPageViewModel()
        {
            EditCommand = new Command<Tarefa>(OnEditCommandExecuted);
            DeleteCommand = new Command(OnDeleteCommandExecuted);
        }

        private void OnEditCommandExecuted(Tarefa tarefa)
        {
            Console.WriteLine(tarefa.Titulo);
            MessagingCenter.Send(this, "NavigateToEdit", tarefa);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        public ICommand DeleteCommand { get; }

        private async void OnDeleteCommandExecuted()
        {
            MessagingCenter.Send(this, "DeleteTask", TarefaSelecionada);
            TarefaSelecionada = null;

            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

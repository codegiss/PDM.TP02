using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TarefasApp.Models;

namespace TarefasApp.ViewModels
{
    internal class EditPageViewModel : BindableObject
    {
        private string _titulo;
        private string _descricao;
        private int _prioridade;
        private DateTime _dataCriacao;
        private Tarefa _tarefaSelecionada;
        public EditPageViewModel()
        {
            SaveCommand = new Command(SaveTask);
        }
        public EditPageViewModel(Tarefa tarefa)
        {
            _tarefaSelecionada = tarefa;
            Titulo = tarefa.Titulo;
            Descricao = tarefa.Descricao;
            Prioridade = tarefa.Prioridade;
            DataCriacao = tarefa.DataCriacao;

            SaveCommand = new Command(SaveTask);
        }
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }
        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                OnPropertyChanged();
            }
        }
        public int Prioridade
        {
            get => _prioridade;
            set
            {
                _prioridade = value;
                OnPropertyChanged();
            }
        }
        public DateTime DataCriacao
        {
            get => _dataCriacao;
            set
            {
                _dataCriacao = value.Date;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; private set; }
        public Tarefa TarefaSelecionada
        {
            get => _tarefaSelecionada;
            set
            {
                _tarefaSelecionada = value;
                if(_tarefaSelecionada != null)
                {
                    Titulo = _tarefaSelecionada.Titulo;
                    Descricao = _tarefaSelecionada.Descricao;
                    Prioridade = _tarefaSelecionada.Prioridade;
                }
                OnPropertyChanged();
            }
        }
        private async void SaveTask()
        {
            MessagingCenter.Send(this, "EditTask", TarefaSelecionada);

            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}

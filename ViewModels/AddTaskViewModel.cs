using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TarefasApp.Models;

namespace TarefasApp.ViewModels
{
    internal class AddTaskViewModel : BindableObject
    {
        private string _titulo;
        private string _descricao;
        private int _prioridade;

        public AddTaskViewModel()
        {
            AddTaskCommand = new Command(AddTask);
            Prioridade = 3;
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
        public ICommand AddTaskCommand { get; }
        private void AddTask()
        {
            if(!string.IsNullOrWhiteSpace(Titulo)&& !string.IsNullOrWhiteSpace(Descricao))
            {
                var novaTarefa = new Tarefa
                {
                    Titulo = Titulo,
                    Descricao = Descricao,
                    DataCriacao = DateTime.Now,
                    Prioridade = Prioridade
                };

                MessagingCenter.Send(this, "AddTask", novaTarefa);

                Titulo = string.Empty;
                Descricao = string.Empty;
                Prioridade = 3;
            }
        }
    }
}

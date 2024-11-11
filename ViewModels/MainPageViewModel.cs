using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Models;
using TarefasApp.Views;
using TarefasApp.ViewModels;
using System.Windows.Input;

namespace TarefasApp.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        private ObservableCollection<Tarefa> _tarefas;
        public ObservableCollection<Tarefa> Tarefas
        {
            get => _tarefas;
            set
            {
                _tarefas = value;
                OnPropertyChanged(nameof(Tarefas));
            }
        }
        public MainPageViewModel()
        {
            Tarefas = new ObservableCollection<Tarefa>();

            EditTaskCommand = new Command<Tarefa>(EditTask);
            DeleteCommand = new Command<Tarefa>(OnDeleteCommandExecuted);

            MessagingCenter.Subscribe<AddTaskViewModel, Tarefa>(this, "AddTask", (sender, novaTarefa) =>
            {
                Tarefas.Add(novaTarefa);
            });

            MessagingCenter.Subscribe<EditPageViewModel, Tarefa>(this, "EditTask", (sender, tarefaAtualizada) =>
            {
                var tarefa = Tarefas.FirstOrDefault(t => t.Id == tarefaAtualizada.Id);

                if (tarefa != null)
                {
                    tarefa.Titulo = tarefaAtualizada.Titulo;
                    tarefa.Descricao = tarefaAtualizada.Descricao;
                    tarefa.DataCriacao = tarefaAtualizada.DataCriacao;
                    tarefa.Prioridade = tarefaAtualizada.Prioridade;

                    Console.WriteLine($"Tarefa editada: {tarefa.Titulo}");

                    OnPropertyChanged(nameof(Tarefas));
                    //Tarefas = new ObservableCollection<Tarefa>(Tarefas);
                }
            });

            SubscribeToDelete();
        }
        private Tarefa _tarefaSelecionada;
        public Tarefa TarefaSelecionada
        {
            get => _tarefaSelecionada;
            set
            {
                _tarefaSelecionada = value;
                OnPropertyChanged();
            }
        }
        public Command<Tarefa> EditTaskCommand { get; }
        public ICommand DeleteCommand { get; }
        private void EditTask(Tarefa tarefa)
        {
            var index = Tarefas.IndexOf(tarefa);
            if (index >= 0)
            {
                Tarefas[index] = tarefa;
                MessagingCenter.Send(this, "EditTask", tarefa);
            }
        }
        private void OnDeleteCommandExecuted(Tarefa tarefa)
        {
            Tarefas.Remove(tarefa);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task SubscribeToDelete()
        {
            MessagingCenter.Subscribe<DetailsPageViewModel, Tarefa>(this, "DeleteTask", async (sender, tarefa) =>
            {
                bool resposta = await Application.Current.MainPage.DisplayAlert(
                    "Confirmação",
                    $"Você tem certeza que deseja excluir a tarefa '{tarefa.Titulo}'?",
                    "Sim",
                    "Não"
                );

                if (resposta)
                {
                    Tarefas.Remove(tarefa);
                    await Application.Current.MainPage.DisplayAlert("Aviso", $"Tarefa {tarefa.Titulo} apagada.", "OK");
                }
            });
        }
    }
}

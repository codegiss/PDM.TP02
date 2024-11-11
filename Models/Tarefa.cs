using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasApp.Models
{
    public class Tarefa
    {
        public int Id { get; private set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Prioridade { get; set; }
        private static int _id = 1;
        public Tarefa(string titulo, string descricao, DateTime dataCriacao, int prioridade)
        {
            Id = _id++;
            Titulo = titulo;
            Descricao = descricao;
            DataCriacao = dataCriacao.Date;
            Prioridade = prioridade;
        }
        public Tarefa()
        {

        }
    }
}

using ListaTarefas.Models.DTOs;

namespace ListaTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public DateTime DataDeCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataExclusao { get; set; }
        public bool Deletado => DataExclusao != null; // soft delete

        public TarefaResponseDto ToDto(Tarefa tarefa)
        {
            return new TarefaResponseDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                DataDeCriacao = tarefa.DataDeCriacao
            };
        }
    }

    public enum StatusTarefa
    {
        Pendente = 0,
        EmAndamento = 1,
        Concluido = 2
    }
}

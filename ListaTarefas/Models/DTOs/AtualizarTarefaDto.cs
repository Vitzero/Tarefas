using System.ComponentModel.DataAnnotations;

namespace ListaTarefas.Models.DTOs
{
    public class AtualizarTarefaDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MinLength(3, ErrorMessage = "O título deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O título deve ter no máximo 60 caracteres.")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(300, ErrorMessage = "A descrição não pode passar de 300 caracteres.")]
        public string? Descricao { get; set; }
        public StatusTarefa? Status { get; set; }

        public Tarefa ToEntity(Tarefa tarefaExistente)
        {
            tarefaExistente.Titulo = Titulo ?? tarefaExistente.Titulo;
            tarefaExistente.Descricao = Descricao ?? tarefaExistente.Descricao;
            tarefaExistente.Status = Status ?? tarefaExistente.Status;

            return tarefaExistente;
        }
    }
}

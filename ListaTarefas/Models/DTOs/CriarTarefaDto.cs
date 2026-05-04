using System.ComponentModel.DataAnnotations;

namespace ListaTarefas.Models.DTOs
{
    public class CriarTarefaDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MinLength(3, ErrorMessage = "O título deve ter no mínimo 3 caracteres.")]
        [MaxLength(60, ErrorMessage = "O título deve ter no máximo 60 caracteres.")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(300, ErrorMessage = "A descrição não pode passar de 300 caracteres.")]
        public required string Descricao { get; set; }
        public required StatusTarefa Status { get; set; }

        public Tarefa ToEntity()
        {
            return new Tarefa
            {
                Titulo = Titulo,
                Descricao = Descricao,
                Status = Status
            };
        }
    }
}



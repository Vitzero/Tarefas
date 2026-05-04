namespace ListaTarefas.Models.DTOs
{
    public class TarefaResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
        public DateTime DataDeCriacao { get; set; } = DateTime.UtcNow;
    }
}

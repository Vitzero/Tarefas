using ListaTarefas.Models;

namespace ListaTarefas.Repositories
{
    public interface ITarefasRepository 
    {
        Task<Tarefa?> BuscarPorId(int id);
        Task<IEnumerable<Tarefa>> BuscarAsync(StatusTarefa? status, string? search);
        Task Deletar(Tarefa tarefa);
        Task Atualizar(Tarefa tarefa);
        Task Criar(Tarefa tarefa);
    }
}

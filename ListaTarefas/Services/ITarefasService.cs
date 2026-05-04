using ListaTarefas.Models;
using ListaTarefas.Models.DTOs;

namespace ListaTarefas.Services
{
    public interface ITarefasService
    {
        Task<IEnumerable<TarefaResponseDto>> Listar(StatusTarefa? status, string? search);
        Task Criar(CriarTarefaDto dto);
        Task Atualizar(int id, AtualizarTarefaDto dto);
        Task Deletar(int id);
    }
}

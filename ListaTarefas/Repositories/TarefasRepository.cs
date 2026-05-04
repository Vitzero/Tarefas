using ListaTarefas.Data;
using ListaTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefas.Repositories
{
    public class TarefasRepository : ITarefasRepository
    {
        private readonly AppDbContext _context;

        public TarefasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa?> BuscarPorId(int id)
        {
            return await _context.Tarefas
                .Where(t => t.DataExclusao == null)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> BuscarAsync(StatusTarefa? status, string? search)
        {
            var query = _context.Tarefas.AsQueryable();

            query = query.Where(t => t.DataExclusao == null);

            if (status.HasValue)
                query = query.Where(t => t.Status == status);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Titulo!.Contains(search) || t.Descricao!.Contains(search));

            query = query.OrderBy(t => t.Status);

            return await query.ToListAsync();
        }

        public async Task Atualizar(Tarefa tarefa)
        {
             _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Criar(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Tarefa tarefa)
        {
            tarefa.DataExclusao = DateTime.UtcNow;

            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}

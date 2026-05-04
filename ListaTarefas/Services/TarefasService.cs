using System;
using ListaTarefas.Models;
using ListaTarefas.Models.DTOs;
using ListaTarefas.Repositories;

namespace ListaTarefas.Services
{
    public class TarefasService : ITarefasService
    {
        private ITarefasRepository _repository;
        public TarefasService(ITarefasRepository repo)
        {
            _repository = repo;
        }

        private void Validar(Tarefa tarefa)
        {
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
            {
                throw new ArgumentException("O título da tarefa é obrigatório.");
            }

            if (tarefa.Status == StatusTarefa.Concluido && string.IsNullOrWhiteSpace(tarefa.Titulo))
            {
                throw new InvalidOperationException("Não é possível concluir uma tarefa sem título válido.");
            }

            if(tarefa.Status == StatusTarefa.Concluido && tarefa.DataExclusao != null)
            {
                throw new InvalidOperationException("Não é possível concluir uma tarefa sem título válido.");
            }
        }

        public async Task Atualizar(int id, AtualizarTarefaDto dto)
        {
            var tarefa = await _repository.BuscarPorId(id);

            if (tarefa == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada.");
            }

            tarefa = dto.ToEntity(tarefa);

            Validar(tarefa);

            await _repository.Atualizar(tarefa);
        }

        public async Task Criar(CriarTarefaDto dto)
        {
            Tarefa tarefa = new Tarefa();

            tarefa = dto.ToEntity();

            
            Validar(tarefa);

            DateTime utcNow = DateTime.UtcNow;
            string fusoHorarioId = OperatingSystem.IsWindows() ? "E. South America Standard Time" : "America/Sao_Paulo";
            TimeZoneInfo brTimeZone = TimeZoneInfo.FindSystemTimeZoneById(fusoHorarioId);
            DateTime brDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, brTimeZone);
            tarefa.DataDeCriacao = brDateTime;

            await _repository.Criar(tarefa);
        }

        public async Task Deletar(int id)
        {
            var tarefa = await _repository.BuscarPorId(id);

            if (tarefa == null)
            {
                throw new KeyNotFoundException($"Tarefa com ID {id} não encontrada.");
            }

            Validar(tarefa);

            await _repository.Deletar(tarefa);
        }

        public async Task<IEnumerable<TarefaResponseDto>> Listar(StatusTarefa? status, string? search)
        {
            var lista = await _repository.BuscarAsync(status, search);

            return lista.Select(x => x.ToDto(x)).AsEnumerable();
        }
    }
}

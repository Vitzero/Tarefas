using ListaTarefas.Models;
using ListaTarefas.Models.DTOs;
using ListaTarefas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private ITarefasService _service;

        public TarefasController(ITarefasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaResponseDto>>> Listar(StatusTarefa? status, string? search )
        {
            var tarefas = await _service.Listar(status, search);

            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarTarefaDto dto)
        {
            await _service.Criar(dto);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarTarefaDto dto)
        {
            await _service.Atualizar(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _service.Deletar(id);

            return NoContent();
        }

    }
}

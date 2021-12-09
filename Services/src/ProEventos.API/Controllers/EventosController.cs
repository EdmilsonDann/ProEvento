using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService eventoService;
        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet]
        //o retorno coloco IActionResult por que ao inves do retorno tradicional, o IActionResult  me permite retornar
        //o status code 200, 500
        public async Task<IActionResult> Get()
        {
            try
            {
                 var eventos = await eventoService.GetAllEventosAsync(true);

                 if(eventos == null) return NotFound("Nenhum evento encontrado.");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

         [HttpGet("id/{id}")]
        //**** o retorno coloco ActionResult<Evento> por que ao inves do retorno tradicional, o ActionResult  me permite retornar
        //o status code 200, 500 e além disso tem a possibilidade de retornar o tipo. 
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                 var eventos = await eventoService.GetEventoByIdAsync(id, true);

                 if(eventos == null) return NotFound("Evento não encontrado por id.");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        //[HttpGet("{tema}/tema")] pode ser tanto na frente com atras para o http fazer a diferença.. pois deixar somente {id} ou {tema}
        //ele pode até tentar identificar.. mas é melhor voce mostrar ao http, para ter duas rotas diferentes.
         [HttpGet("tema/{tema}")]
        //**** o retorno coloco ActionResult<Evento> por que ao inves do retorno tradicional, o ActionResult  me permite retornar
        //o status code 200, 500 e além disso tem a possibilidade de retornar o tipo. 
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                 var eventos = await eventoService.GetAllEventosByTemaAsync(tema, true);

                 if(eventos == null) return NotFound("Eventos por tema não encontrados.");

                 return Ok(eventos);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        
         [HttpPost]
        //**** o retorno coloco ActionResult<Evento> por que ao inves do retorno tradicional, o ActionResult  me permite retornar
        //o status code 200, 500 e além disso tem a possibilidade de retornar o tipo. 
        public async Task<IActionResult> Post(Evento evento)
        {
            try
            {
                 var retorno = await eventoService.AddEventos(evento);

                 if(evento == null) return BadRequest("Erro ao tentar adicionar evento");

                 return Ok(retorno);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {ex.Message}");
            }
        }

         [HttpPut("{id}")]
        //**** o retorno coloco ActionResult<Evento> por que ao inves do retorno tradicional, o ActionResult  me permite retornar
        //o status code 200, 500 e além disso tem a possibilidade de retornar o tipo. 
        public async Task<IActionResult> Put(int id, Evento evento)
        {
            try
            {
                 var retorno = await eventoService.UpdateEvento(id, evento);

                 if(evento == null) return BadRequest("Erro ao tentar adicionar evento");

                 return Ok(retorno);
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        //**** o retorno coloco ActionResult<Evento> por que ao inves do retorno tradicional, o ActionResult  me permite retornar
        //o status code 200, 500 e além disso tem a possibilidade de retornar o tipo. 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await eventoService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Erro ao tentar apagar o evento");                 
            }
            catch (Exception ex)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar apagar o evento. Erro: {ex.Message}");
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using ApiCatalogoJogos.Services;
using System.ComponentModel.DataAnnotations;
using ApiCatalogoJogos.Exceptions;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        // Query ex: api?id=123
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter(
            [FromQuery, Range(1, int.MaxValue)] int pagina = 1,
            [FromQuery, Range(1, 50)] int quantidade = 5
        )
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if (jogos.Count == 0)
                return NoContent();
            
            //Testando o middleware criado para a nossa api
            // throw new Exception();

            return Ok(jogos);
        }

        // Rota ex: api/123
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoService.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }


        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);

                return Ok(jogo);
            }
            catch (JogoCadastradoException e)
            {
                return UnprocessableEntity("Ja existe umA entidade com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("Nao existe este jogo");
            }
        }


        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("Nao existe este jogo");
            }
        }


        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("Nao existe este jogo");
            }
        }
    }
}
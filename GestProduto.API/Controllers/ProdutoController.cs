using Microsoft.AspNetCore.Mvc;
using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;
using GestProduto.API.Exceptions;
using GestProduto.API.Service;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using GestProduto.API.Domain.Entity;

namespace GestProduto.API.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            this.produtoService = produtoService;
        }

        // GET /produtos/categoria
        [HttpGet("categoria")]
        [Authorize]
        public ActionResult<List<CategoriaResponse>> ListarCategoriasDeProduto()
        {
            try
            {
                List<CategoriaResponse> categorias = produtoService.ListarCategoriasDeProduto();
                return Ok(categorias);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET /produtos/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ProdutoResponse> BuscarPorId(int id)
        {
            try
            {
                ProdutoResponse produto = produtoService.BuscaPorId(id);
                return Ok(produto);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET /produtos?id={id}&nome={nome}&categoriaId={categoriaId}&disponivel={disponivel}
        [HttpGet]
        [Authorize]
        public ActionResult<List<ProdutoResponse>> BuscarProdutos([FromQuery] int? id, [FromQuery] string? nome, [FromQuery] int? categoriaId, [FromQuery] bool? disponivel)
        {
            try
            {
                List<ProdutoResponse> produtos = produtoService.BuscarProdutos(id, nome, categoriaId, disponivel);
                return Ok(produtos);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST /produtos
        [HttpPost]
        [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN) + "," + nameof(PerfilUsuarioEnum.GESTOR) + "," + nameof(PerfilUsuarioEnum.FUNCIONARIO))]
        public ActionResult<ProdutoCadastroResponse> Cadastrar([FromBody] ProdutoRequest produto)
        {
            try
            {
                ProdutoCadastroResponse produtoCadastro = produtoService.Cadastrar(produto);
                return CreatedAtAction(nameof(BuscarPorId), new { id = produtoCadastro.Id }, produtoCadastro);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PUT /produtos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN) + "," + nameof(PerfilUsuarioEnum.GESTOR) + "," + nameof(PerfilUsuarioEnum.FUNCIONARIO))]
        public ActionResult Atualizar(int id, [FromBody] ProdutoRequest produto)
        {
            try
            {
                produtoService.Atualizar(id, produto);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PATCH /produtos/{id}?quantidade={quantidade}
        [HttpPatch("{id}")]
        [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN) + "," + nameof(PerfilUsuarioEnum.GESTOR) + "," + nameof(PerfilUsuarioEnum.FUNCIONARIO))]
        public ActionResult AtualizarEstoque(int id, [FromQuery] int? quantidade)
        {
            try
            {
                produtoService.AtualizarEstoque(id, quantidade);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // DELETE /produtos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN) + "," + nameof(PerfilUsuarioEnum.GESTOR))]
        public ActionResult Deletar(int id)
        {
            try
            {
                produtoService.Deletar(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}

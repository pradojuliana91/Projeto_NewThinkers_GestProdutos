using Microsoft.AspNetCore.Mvc;
using GestProduto.API.Controllers.Request;
using GestProduto.API.Controllers.Response;
using GestProduto.API.Exceptions;
using GestProduto.API.Service;
using System.Net;

namespace GestProduto.API.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        // POST /login
        [HttpPost]
        public ActionResult<ProdutoCadastroResponse> Cadastrar([FromBody] LoginRequest login)
        {
            try
            {
                TokenResponse tokenResponse = usuarioService.Login(login);
                return Ok(tokenResponse);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}

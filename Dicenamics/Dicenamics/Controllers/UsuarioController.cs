using Microsoft.AspNetCore.Mvc;
using Dicenamics.Models;
using Dicenamics.Data;
namespace Dicenamics.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDatabase _ctx;

        public UsuarioController(AppDatabase context)
        {
            _ctx = context;
        }

        [HttpPost]
        public IActionResult VerificarUsuarioExistente(string username)
        {
            bool usuarioExiste = _ctx.Usuarios.Any(u => u.Username == username);

            return Ok(usuarioExiste);
        }
    }
}

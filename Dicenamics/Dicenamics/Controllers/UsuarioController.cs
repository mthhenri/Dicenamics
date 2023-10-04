using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var usuarios = await _ctx.Usuarios.ToListAsync();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _ctx.Add(usuario);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _ctx.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Update(usuario);
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExiste(usuario.UsuarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // Metodo criado para para buscar username de usuario e caso ja existe ele retornar 
        private bool UsuarioExiste(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var usuario = await _ctx.Usuarios.FindAsync(id);
            _ctx.Usuarios.Remove(usuario);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuscarPorId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

                var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(m => m.UsuarioId == id);
                if (usuario == null)
            {
                return NotFound();
            }

                return View(usuario);
        }

        public async Task<IActionResult> BuscarPorUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

                var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(m => m.Username == username);
                if (usuario == null)
            {
                return NotFound();
            }

                return View(usuario);
        }
    }
}

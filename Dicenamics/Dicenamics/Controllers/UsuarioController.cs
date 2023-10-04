using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dicenamics.Models;
using Dicenamics.Data;

namespace Dicenamics.Controllers;

[ApiController]
[Route("dicenamics/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly AppDatabase _ctx;

    public UsuarioController(AppDatabase context)
    {
        _ctx = context;
    }

    //Colocar metodo http com rota
    public async Task<IActionResult> ListarUsuarios()
    {
        //Try catch
        var usuarios = await _ctx.Usuarios.ToListAsync();
        return View(usuarios);
    }

    //Colocar metodo http com rota
    public IActionResult Criar()
    {
        //??
        return View();
    }

    [HttpPost] //Rota
    public async Task<IActionResult> Criar(Usuario usuario)
    {
        //try catch
        //??
        if (ModelState.IsValid)
        {
            _ctx.Add(usuario);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(usuario);
    }

    //Colocar metodo http com rota
    public async Task<IActionResult> Editar(int? id)
    {
        //try catch
        //??
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

    [HttpPost] //Rota
    public async Task<IActionResult> Editar(int id, Usuario usuario)
    {
        //Try catch
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
    //Colocar metodo http com rota
    private bool UsuarioExiste(int usuarioId)
    {
        //??
        throw new NotImplementedException();
    }

    //Colocar metodo http com rota
    public async Task<IActionResult> Excluir(int? id)
    {
        //try catch
        //??
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

    [HttpPost, ActionName("Excluir")] //??
    public async Task<IActionResult> ExcluirConfirmado(int id)
    {
        //try catch
        //??
        var usuario = await _ctx.Usuarios.FindAsync(id);
        _ctx.Usuarios.Remove(usuario);
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Colocar metodo http e rota
    public async Task<IActionResult> BuscarPorId(int? id)
    {
        //try catch
        //??
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

    // Colocar metodo http e rota
    public async Task<IActionResult> BuscarPorUsername(string username)
    {
        //try catch
        //??
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


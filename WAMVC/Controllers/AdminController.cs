using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAMVC.Data;
using WAMVC.Models;

namespace WAMVC.Controllers
{
    [Authorize(Policy = "AdminOnly")] // Solo administradores pueden acceder
    public class AdminController : Controller
    {
        private readonly ArtesaniasDBContext _context;

        public AdminController(ArtesaniasDBContext context)
        {
     _context = context;
        }

  // GET: Admin/Usuarios
        public async Task<IActionResult> Usuarios()
        {
     var usuarios = await _context.Usuarios.ToListAsync();
return View(usuarios);
        }

    // GET: Admin/EditarUsuario/5
      public async Task<IActionResult> EditarUsuario(int? id)
        {
         if (id == null)
            {
        return NotFound();
    }

  var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.Roles = new List<string> { "Admin", "Empleado", "Usuario", "Cliente" };
 return View(usuario);
        }

 // POST: Admin/EditarUsuario/5
      [HttpPost]
   [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(int id, Usuario usuario)
        {
       if (id != usuario.Id)
  {
            return NotFound();
            }

          if (ModelState.IsValid)
       {
     try
     {
     var usuarioExistente = await _context.Usuarios.FindAsync(id);
       if (usuarioExistente == null)
             {
      return NotFound();
      }

              // Actualizar solo email, rol y estado activo (no la contraseña)
             usuarioExistente.Email = usuario.Email;
 usuarioExistente.Rol = usuario.Rol;
          usuarioExistente.Activo = usuario.Activo;

         _context.Update(usuarioExistente);
         await _context.SaveChangesAsync();
         TempData["SuccessMessage"] = $"Usuario '{usuario.Email}' actualizado exitosamente.";
     }
          catch (DbUpdateConcurrencyException)
             {
    if (!UsuarioExists(usuario.Id))
   {
         return NotFound();
       }
              else
         {
      throw;
  }
     }
       return RedirectToAction(nameof(Usuarios));
            }

   ViewBag.Roles = new List<string> { "Admin", "Empleado", "Usuario", "Cliente" };
         return View(usuario);
        }

        // POST: Admin/ToggleUsuario/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleUsuario(int id)
  {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
       {
   usuario.Activo = !usuario.Activo;
   _context.Update(usuario);
 await _context.SaveChangesAsync();
 
     string estado = usuario.Activo ? "activado" : "desactivado";
           TempData["SuccessMessage"] = $"Usuario '{usuario.Email}' {estado} exitosamente.";
  }

  return RedirectToAction(nameof(Usuarios));
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
     {
       var stats = new
    {
     TotalUsuarios = await _context.Usuarios.CountAsync(),
           UsuariosActivos = await _context.Usuarios.CountAsync(u => u.Activo),
    TotalClientes = await _context.Clientes.CountAsync(),
    TotalProductos = await _context.Productos.CountAsync(),
        TotalPedidos = await _context.Pedidos.CountAsync(),
       PedidosHoy = await _context.Pedidos.CountAsync(p => p.FechaPedido.Date == DateTime.Today)
       };

       return View(stats);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
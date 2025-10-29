using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WAMVC.Data;
using WAMVC.Models;
using System.Security.Claims;

namespace WAMVC.Controllers
{
[Authorize] // Usuarios autenticados pueden acceder
    public class PedidoController : Controller
    {
        private readonly ArtesaniasDBContext _context;

    public PedidoController(ArtesaniasDBContext context)
      {
     _context = context;
     }

 // GET: Pedido - Solo admins y empleados pueden ver todos los pedidos
      [Authorize(Policy = "AdminOrEmpleado")]
     public async Task<IActionResult> Index()
        {
    var pedidos = await _context.Pedidos
 .Include(p => p.Cliente)
   .ToListAsync();
   return View(pedidos);
      }

        // GET: Pedido/Details - Admins/empleados ven cualquier pedido, usuarios solo los suyos
        public async Task<IActionResult> Details(int? id)
        {
   if (id == null)
  {
    return NotFound();
            }

            var pedidoModel = await _context.Pedidos
    .Include(p => p.Cliente)
                .Include(p => p.DetallePedidos)
        .ThenInclude(d => d.Producto)
       .FirstOrDefaultAsync(m => m.Id == id);
            
   if (pedidoModel == null)
       {
   return NotFound();
       }

            // Verificar si el usuario puede ver este pedido
  if (!User.IsInRole("Admin") && !User.IsInRole("Empleado"))
  {
           var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;
        if (pedidoModel.Cliente?.Email != userEmail)
             {
          return Forbid(); // El usuario solo puede ver sus propios pedidos
        }
        }

            return View(pedidoModel);
        }

   // GET: Pedido/Create - Solo admins y empleados pueden crear pedidos
        [Authorize(Policy = "AdminOrEmpleado")]
        public IActionResult Create()
        {
ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre");
            var pedido = new PedidoModel
      {
      FechaPedido = DateTime.Now
  };
   return View(pedido);
        }

        // POST: Pedido/Create - Solo admins y empleados pueden crear pedidos
  [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrEmpleado")]
        public async Task<IActionResult> Create([Bind("Id,IdCliente,FechaPedido,Direccion,MontoTotal")] PedidoModel pedidoModel)
     {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoModel);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Pedido #{pedidoModel.Id} creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pedidoModel.IdCliente);
            return View(pedidoModel);
        }

        // GET: Pedido/Edit - Solo admins pueden editar pedidos
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos.FindAsync(id);
            if (pedidoModel == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pedidoModel.IdCliente);
            return View(pedidoModel);
        }

        // POST: Pedido/Edit - Solo admins pueden editar pedidos
        [HttpPost]
        [ValidateAntiForgeryToken]
      [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCliente,FechaPedido,Direccion,MontoTotal")] PedidoModel pedidoModel)
        {
            if (id != pedidoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoModel);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Pedido #{pedidoModel.Id} actualizado exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoModelExists(pedidoModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nombre", pedidoModel.IdCliente);
            return View(pedidoModel);
        }

        // GET: Pedido/Delete - Solo admins pueden eliminar pedidos
 [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int? id)
    {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoModel = await _context.Pedidos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (pedidoModel == null)
            {
                return NotFound();
            }

            return View(pedidoModel);
        }

        // POST: Pedido/Delete - Solo admins pueden eliminar pedidos
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteConfirmed(int id)
 {
            var pedidoModel = await _context.Pedidos.FindAsync(id);
            if (pedidoModel != null)
            {
                _context.Pedidos.Remove(pedidoModel);
                TempData["SuccessMessage"] = $"Pedido #{pedidoModel.Id} eliminado exitosamente.";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoModelExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
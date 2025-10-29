using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WAMVC.Controllers
{
    [Authorize] // Requiere usuario autenticado
    public class TestAccesoController : Controller
    {
    public IActionResult Index()
   {
     ViewBag.UsuarioActual = User.Identity?.Name ?? "No autenticado";
ViewBag.RolActual = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "Sin rol";
            ViewBag.EsAdmin = User.IsInRole("Admin");
      ViewBag.EsEmpleado = User.IsInRole("Empleado");
     ViewBag.EsUsuario = User.IsInRole("Usuario");
         
     return View();
        }

        [Authorize(Policy = "AdminOnly")]
     public IActionResult SoloAdmin()
    {
   return View();
        }

    [Authorize(Policy = "AdminOrEmpleado")]
        public IActionResult AdminOEmpleado()
  {
     return View();
        }

      [Authorize(Policy = "ClienteOnly")]
     public IActionResult SoloCliente()
        {
            return View();
        }

      [AllowAnonymous]
        public IActionResult AccesoPublico()
    {
            return View();
     }
    }
}

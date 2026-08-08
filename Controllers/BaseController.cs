using Practica_03.Servicios;
using System;
using System.Web.Mvc;

namespace Practica_03.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UtilitarioService utilitario = new UtilitarioService();

        protected ActionResult MostrarError(Exception ex, string lugar)
        {
            utilitario.RegistrarErrorBitacora(ex.Message, lugar);
            return View("Error");
        }
    }
}


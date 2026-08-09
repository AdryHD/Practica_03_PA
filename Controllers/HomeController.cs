using Practica_03.EF;
using Practica_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Practica_03.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Consulta()
        {
            try
            {
                using (var context = new Practica03Entities())
                {
                    var datos = context.Principal
                        .OrderBy(item => item.Estado == "Pendiente" ? 0 : 1)
                        .ThenBy(item => item.Id_Compra)
                        .ToList();

                    return View(datos);
                }
            }
            catch (Exception ex)
            {
                return MostrarError(ex, "Consulta");
            }
        }

        [HttpGet]
        public ActionResult Registro()
        {
            try
            {
                var model = new RegistroAbonoModel
                {
                    ComprasPendientes = CargarComprasPendientes(),
                    SaldoAnterior = 0
                };

                ViewBag.ScriptFile = "registro.js";
                return View(model);
            }
            catch (Exception ex)
            {
                return MostrarError(ex, "Registro_GET");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro([Bind(Exclude = "SaldoAnterior,ComprasPendientes")] RegistroAbonoModel model)
        {
            try
            {
                model.ComprasPendientes = CargarComprasPendientes();
                ViewBag.ScriptFile = "registro.js";

                ModelState.Remove("SaldoAnterior");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (!model.IdCompra.HasValue || model.Abono <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Debe completar la información del formulario.");
                    return View(model);
                }

                using (var context = new Practica03Entities())
                {
                    var compra = context.Principal
                        .FirstOrDefault(item => item.Id_Compra == model.IdCompra.Value);

                    if (compra == null)
                    {
                        ModelState.AddModelError(string.Empty, "No se encontró la compra seleccionada.");
                        return View(model);
                    }

                    if (compra.Estado != "Pendiente")
                    {
                        ModelState.AddModelError(string.Empty, "La compra seleccionada no está pendiente.");
                        return View(model);
                    }

                    model.SaldoAnterior = compra.Saldo;

                    if (model.Abono > compra.Saldo)
                    {
                        ModelState.AddModelError("Abono", "El abono no puede ser mayor al saldo anterior.");
                        return View(model);
                    }

                    var nuevoSaldo = compra.Saldo - model.Abono;

                    context.Abonos.Add(new Abono
                    {
                        Id_Compra = compra.Id_Compra,
                        Monto = model.Abono,
                        Fecha = DateTime.Now
                    });

                    compra.Saldo = nuevoSaldo;
                    if (nuevoSaldo == 0)
                    {
                        compra.Estado = "Cancelado";
                    }

                    var response = context.SaveChanges();

                    if (response <= 0)
                    {
                        ModelState.AddModelError(string.Empty, "No se pudo registrar el abono.");
                        return View(model);
                    }

                    TempData["Exito"] = "El abono se registró satisfactoriamente.";
                    return RedirectToAction("Consulta", "Home");
                }
            }
            catch (Exception ex)
            {
                return MostrarError(ex, "Registro_POST");
            }
        }

        [HttpGet]
        public JsonResult ObtenerSaldoAnterior(long idCompra)
        {
            try
            {
                using (var context = new Practica03Entities())
                {
                    var compra = context.Principal
                        .Where(item => item.Id_Compra == idCompra && item.Estado == "Pendiente")
                        .Select(item => new
                        {
                            item.Saldo
                        })
                        .FirstOrDefault();

                    if (compra == null)
                    {
                        return Json(new
                        {
                            ok = false,
                            mensaje = "No se encontró la compra pendiente."
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new
                    {
                        ok = true,
                        saldo = compra.Saldo
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                utilitario.RegistrarErrorBitacora(ex.Message, "ObtenerSaldoAnterior");
                return Json(new
                {
                    ok = false,
                    mensaje = "No fue posible obtener el saldo anterior."
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Consulta", "Home");
        }

        private List<SelectListItem> CargarComprasPendientes()
        {
            using (var context = new Practica03Entities())
            {
                return context.Principal
                    .Where(item => item.Estado == "Pendiente")
                    .OrderBy(item => item.Id_Compra)
                    .Select(item => new SelectListItem
                    {
                        Value = item.Id_Compra.ToString(),
                        Text = "Compra " + item.Id_Compra + " - " + item.Descripcion
                    })
                    .ToList();
            }
        }
    }
}

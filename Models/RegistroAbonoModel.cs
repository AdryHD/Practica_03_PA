using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Practica_03.Models
{
    public class RegistroAbonoModel
    {
        [Required(ErrorMessage = "Debe seleccionar una compra")]
        [Display(Name = "Compra")]
        public long? IdCompra { get; set; }

        [Display(Name = "Saldo Anterior")]
        public decimal SaldoAnterior { get; set; }

        [Required(ErrorMessage = "El campo Abono es requerido")]
        [Range(0.01, 999999999999.0, ErrorMessage = "El abono debe ser mayor que cero")]
        [Display(Name = "Abono")]
        public decimal Abono { get; set; }

        public List<SelectListItem> ComprasPendientes { get; set; }
    }
}


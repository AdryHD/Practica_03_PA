using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica_03.EF
{
    [Table("Abonos")]
    public partial class Abono
    {
        [Key]
        public long Id_Abono { get; set; }

        public long Id_Compra { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("Id_Compra")]
        public virtual Principal Principal { get; set; }
    }
}


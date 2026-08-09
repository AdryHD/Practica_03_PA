using System;

namespace Practica_03.EF
{
    public partial class Abono
    {
        public long Id_Compra { get; set; }
        public long Id_Abono { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public virtual Principal Principal { get; set; }
    }
}

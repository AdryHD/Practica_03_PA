using System.Collections.Generic;

namespace Practica_03.EF
{
    public partial class Principal
    {
        public Principal()
        {
            Abonos = new HashSet<Abono>();
        }

        public long Id_Compra { get; set; }
        public decimal Precio { get; set; }
        public decimal Saldo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Abono> Abonos { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practica_03.EF
{
    [Table("Principal")]
    public partial class Principal
    {
        public Principal()
        {
            Abonos = new HashSet<Abono>();
        }

        [Key]
        public long Id_Compra { get; set; }

        public decimal Precio { get; set; }

        public decimal Saldo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(100)]
        public string Estado { get; set; }

        public virtual ICollection<Abono> Abonos { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfumeApi.Models
{
    [Table("perfumes")] // Nombre de la tabla en tu Postgres
    public class Perfume
    {
        [Key]
        [Column("idperfume")]
        public long Id { get; set; }

        [Column("nombreperfume")]
        public string Nombre { get; set; } = string.Empty;

        [Column("marca")]
        public string Marca { get; set; } = string.Empty;

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        // Agregamos estos porque estaban en tu imagen
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("imagen_url")]
        public string? ImagenUrl { get; set; }
    }
}
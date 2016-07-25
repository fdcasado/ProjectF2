using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectF2.Models
{
    public class Modelo
    {
        [Key]
        public int ModeloId { get; set; }

        [Required]
        public string NomeModelo  { get; set; }

        [Required]
        public int MarcaId { get; set; }
        
        [Display(Name = "Marca")]
        public Marca Marca { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }

    }

}

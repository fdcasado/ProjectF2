using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectF2.Models
{
    public class Marca
    {
        [Key]
        public int MarcaId { get; set; }
        [Required]
        public string NomeMarca { get; set; }
        public ICollection<Modelo> Modelos { get; set; }
    }

}

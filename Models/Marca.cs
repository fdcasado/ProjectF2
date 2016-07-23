using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectF2.Models
{
    public class Marca
    {
        [Key]
        public string Key { get; set; }
        public int MarcaId { get; set; }
        public string FipeName { get; set; }
        public string Name { get; set; }
        public ICollection<Modelo> Modelos { get; set; }
    }

}

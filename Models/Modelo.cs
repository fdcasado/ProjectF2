using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectF2.Models
{
    public class Modelo
    {
        [Key]
        public string Key { get; set; }
        public int ModeloId { get; set; }
        public string Name { get; set; }
        public string FipeName { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

    }

}

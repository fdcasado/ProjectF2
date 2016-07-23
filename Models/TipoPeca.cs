using System.Collections.Generic;

namespace ProjectF2.Models
{
    public class TipoPeca
    {
        public int TipoPecaId { get; set; }
        public string NomeTipoPeca { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }

}

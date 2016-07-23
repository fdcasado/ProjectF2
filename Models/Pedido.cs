using System;

namespace ProjectF2.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ModeloId { get; set; }
        public int UsuarioId { get; set; }
        public string AnoModelo { get; set; }
        public int TipoPecaId { get; set; }
        public DateTime DataHora { get; set; }
        public string DescricaoPedido { get; set; }
    }

}

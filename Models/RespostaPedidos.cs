using System;

namespace ProjectF2.Models
{
    public class RespostaPedidos
    {
        public int RespostaPedidosId { get; set; }
        public int PedidoId { get; set; }
        public int LojistaId { get; set; }
        public DateTime DataHoraResposta { get; set; }
        public bool IndNegarResposta { get; set; }
        public string MotivoNegarResposta { get; set; }
        public bool IndSolicitarMaisInfo { get; set; }
        public string DescSolicitarMaisInfo { get; set; }
        public string Resposta { get; set; }
        public string Valor { get; set; }
        public string CondicoesPagamento { get; set; }
        public string CondicoesEntrega { get; set; }
        public bool IndStatusRespondido { get; set; }
    }
}

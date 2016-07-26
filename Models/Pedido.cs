using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectF2.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public int ModeloId { get; set; }
        public string UserId { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Ano Modelo")]
        public string AnoModelo { get; set; }

        [Required]
        [Display(Name = "Tipo da Peça")]
        public int TipoPecaId { get; set; }
        public DateTime DataHora { get; set; }

        [Required]
        [Display(Name = "Descreva seu pedido")]
        public string DescricaoPedido { get; set; }

        public string Status { get; set; }

        public Modelo Modelo { get; set; }
        public Usuario Usuario { get; set; }
        public TipoPeca TipoPeca { get; set; }
    }



    public class ViewPedido
    {
        public int PedidoId { get; set; }
        public DateTime Data { get; set; }
        public string NomeMarca { get; set; }
        public string NomeModelo { get; set; }
        public string NomeTipoPeca { get; set; }
        public string AnoModelo { get; set; }
        public string Status { get; set; }
        public string DescricaoPedido { get; set; }
        public int QtRespostasRecebidas { get; set; }
        public int QtRespostasPendentes { get; set; }
        public string MaisInfo { get; set; }
        public int RespostaId { get; set; }

    }

    public class ListaPedidos
    {
        public IList<ViewPedido> ListagemPedidos { get; set; }
    }


}

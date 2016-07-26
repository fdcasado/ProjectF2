using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectF2.Models
{
    public enum StatusResposta
    {
        [Display(Name = "Pendente")]
        Pendente = 1,

        [Display(Name = "Responder")]
        Responder,

        [Display(Name = "Não Responder")]
        NaoReponder,

        [Display(Name = "Solicitar Mais Info")]
        SolicitarMaisInfo
    }

    public enum MotivoNegarResposta
    {
        [Display(Name = "Selecione o motivo")]
        Motivo_0 = 0,

        [Display(Name = "Motivo 1")]
        Motivo_1 = 1,

        [Display(Name = "Motivo 2")]
        Motivo_2,

        [Display(Name = "Motivo 3")]
        Motivo_3,      
    }

    public class RespostaPedidos
    {
        [Key]
        public int RespostaPedidosId { get; set; }

        public int PedidoId { get; set; }

        public int LojistaId { get; set; }

        public DateTime DataHoraResposta { get; set; }

        public StatusResposta StatusResposta { get; set; }

        public MotivoNegarResposta MotivoNegarResposta { get; set; }

        public string DescSolicitarMaisInfo { get; set; }

        public string Resposta { get; set; }

        [Display(Name = "Condições para pagamento")]
        public string CondicoesPagamento { get; set; }

        [Display(Name = "Condições para entrega")]
        public string CondicoesEntrega { get; set; }
    }

    public class ViewRespostaPedidos : IValidatableObject
    {
        [Key]
        public int RespostaPedidosId { get; set; }

        public int PedidoId { get; set; }

        public int LojistaId { get; set; }

        public DateTime DataHoraResposta { get; set; }

        public StatusResposta StatusResposta { get; set; }

        public MotivoNegarResposta MotivoNegarResposta { get; set; }

        public string DescSolicitarMaisInfo { get; set; }

        public string Resposta { get; set; }

        [Display(Name = "Condições para pagamento")]
        public string CondicoesPagamento { get; set; }

        [Display(Name = "Condições para entrega")]
        public string CondicoesEntrega { get; set; }

        [Display(Name = "Marca")]
        [NotMapped]
        public string NomeMarca { get; set; }

        [Display(Name = "Modelo")]
        [NotMapped]
        public string NomeModelo { get; set; }

        [Display(Name = "Tipo da Peça")]
        [NotMapped]
        public string NomeTipoPeca { get; set; }

        [Display(Name = "Ano Modelo")]
        [NotMapped]
        public string AnoModelo { get; set; }

        [Display(Name = "Descrição do Pedido")]
        [NotMapped]
        public string DescricaoPedido { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StatusResposta == StatusResposta.SolicitarMaisInfo && string.IsNullOrEmpty(DescSolicitarMaisInfo))
                yield return new ValidationResult("Indique quais informações adicionais você precisa", new []{ "DescSolicitarMaisInfo" });

            if (StatusResposta == StatusResposta.NaoReponder && MotivoNegarResposta==0)
                yield return new ValidationResult("Indique o motivo", new[] { "MotivoNegarResposta" });

            if (StatusResposta == StatusResposta.Responder && string.IsNullOrEmpty(Resposta))
                yield return new ValidationResult("Preencha a resposta", new[] { "Resposta" });
        }
    }

    public class ListaViewRespostaPedidos
    {
        public IList<ViewRespostaPedidos> ListagemViewRespostasPedidos { get; set; }
    }
}

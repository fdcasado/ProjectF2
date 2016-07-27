using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectF2.Models
{
    //public enum MotivoNegarResposta
    //{
    //    [Display(Name = "Selecione o motivo")]
    //    Motivo_0 = 0,

    //    [Display(Name = "Motivo 1")]
    //    Motivo_1 = 1,

    //    [Display(Name = "Motivo 2")]
    //    Motivo_2,

    //    [Display(Name = "Motivo 3")]
    //    Motivo_3,      
    //}

    public class RespostaPedidos
    {
        [Key]
        public int RespostaPedidosId { get; set; }

        public int PedidoId { get; set; }

        public int LojistaId { get; set; }

        public bool IndNovoPedido { get; set; }

        public ICollection<Conversa> Conversas { get; set; }
    }

    public class ViewGridRespostaCotacoes
    {
        public int PedidoId { get; set; }
        public int RespostaId { get; set; }
        public bool IndNovaMensagem { get; set; }
        public int LojistaId { get; set; }
        public string NomeLoja { get; set; }

    }

    public class ListaViewGridRespostaCotacoes
    {
        public IList<ViewGridRespostaCotacoes> ListagemViewGridRespostaCotacoes { get; set; }
    }


    public class ViewRespostaPedidos //: IValidatableObject
    {
        [Key]
        public int RespostaPedidosId { get; set; }

        public int PedidoId { get; set; }

        public int LojistaId { get; set; }

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

        [Required]
        [NotMapped]
        public string NovaMensagem { get; set; }

        public IList<Conversa> Conversas { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (IndNegadoLojista && MotivoNegarResposta == 0)
        //        yield return new ValidationResult("Indique o motivo", new[] { "MotivoNegarResposta" });

        //}
    }

    public class ListaViewRespostaPedidos
    {
        public IList<ViewRespostaPedidos> ListagemViewRespostasPedidos { get; set; }
    }
}

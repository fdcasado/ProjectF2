using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF2.Models
{
    public class Conversa
    {
        [Key]
        public int ConversaId { get; set; }

        [Required]
        public int RespostaPedidosId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime DataHoraResposta { get; set; }
        
        [Required]
        public string Mensagem { get; set; }

        public bool IndMensagemLida { get; set; }

        [NotMapped]
        public bool TempIndNovaMensagem { get; set; }

    }
}

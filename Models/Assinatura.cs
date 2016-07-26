using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF2.Models
{
    public class Assinatura
    {
        [Key]
        public int AssinaturaId { get; set; }

        public int LojistaId { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string TipoPeca { get; set; }
    }
}

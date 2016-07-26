using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectF2.Models
{
    public class Lojista
    {
        [Required]
        [Key]
        public int LojistaId { get; set; }

        
        public string UserId { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required]
        [StringLength(120)]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(14)]
        [Display(Name = "Telefone")]
        //[RegularExpression(@"^((11) [9][0-9]{4}-[0-9]{4})|((1[2-9]) [5-9][0-9]{3}-[0-9]{4})|(([2-9][1-9]) [5-9][0-9]{3}-[0-9]{4})$")]
        [Phone]
        public string Telefone { get; set; }

        [NotMapped]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [NotMapped]
        [StringLength(20)]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "A Confirmaçõa da Senha deve ser igual ao que foi digitado no campo Senha.")]
        [Display(Name = "Confirmação da Senha")]
        public string ConfirmacaoSenha { get; set; }

        [Required]
        [StringLength(14)]
        [Display(Name = "Número do CNPJ")]
        //[RegularExpression(@"([0 - 9]{2}[.]?[0 - 9]{3}[.]?[0 - 9]{3}[/]?[0 - 9]{4}[-]?[0 - 9]{2})")]
        public string CNPJ { get; set; }

        public ICollection<Assinatura> Assinaturas { get; set; }

    }

}

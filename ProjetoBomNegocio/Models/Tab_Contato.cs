using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoBomNegocio.Models
{
    
    public class Tab_Contato
    {
        [Key]
        public int idcontato { get; set; }

        [Required(ErrorMessage = "Campo 'Nome' é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        
        public string nome { get; set; }
        
        [Required(ErrorMessage = "Campo 'E-mail' é obrigatório.")]
        [Display(Name = "E-mail")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "Campo 'E-mail' é inválido.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo 'Mensagem' é obrigatório.")]
        [Display(Name = "Mensagem")]
        [StringLength(4000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]

        public string mensagem { get; set; }
        
        [StringLength(200)]
        public string idusuario_cadastro { get; set; }

        public System.DateTime data_cadastro { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoBomNegocio.Models
{
    
    public class Tab_Suporte
    {
        [Key]
        public int idsuporte { get; set; }

        [Required(ErrorMessage = "Campo 'Descreva o problema com detalhes' é obrigatório.")]
        [Display(Name = "Descrição")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        
        public string descricao { get; set; }

        [Required(ErrorMessage = "Campo 'Qual seu sistema operacional?' é obrigatório.")]
        [Display(Name = "Sistema Operacional")]
        public string sistema_operacional { get; set; }

        [Required(ErrorMessage = "Campo 'O problema é recorrente?' é obrigatório.")]
        [Display(Name = "Problema Recorrente")]
        public string problema_recorrente { get; set; }

        [Required(ErrorMessage = "Campo 'Qual é a prioridade do chamado?' é obrigatório.")]
        [Display(Name = "Prioridade")]
        public string prioridade { get; set; }

        [Required(ErrorMessage = "Campo 'E-mail' é obrigatório.")]
        [Display(Name = "E-mail")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [EmailAddress(ErrorMessage = "Campo 'E-mail' é inválido.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo 'DDD Telefone' é obrigatório.")]
        [StringLength(2, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string ddd_telefone { get; set; }

        [Required(ErrorMessage = "Campo 'Telefone' é obrigatório.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string telefone { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.")]
        
        public string melhor_horario { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string status { get; set; }

        [Required(ErrorMessage = "É Necessário concordar com os termos de suporte remoto.")]
        [Display(Name = "Termo")]
        public bool flg_termo_aceito { get; set; }

        [Required]
        [Display(Name = "Data de Abertura")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime data_abertura { get; set; }

        [Required]
        public string idusuario_cadastro { get; set; }

        public System.Nullable<System.DateTime> data_alteracao { get; set; }

        public string idusuario_alteracao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.Nullable<System.DateTime> data_atendimento { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.Nullable<System.DateTime> data_fechamento { get; set; }

        public string descr_solucao { get; set; }

        public System.Nullable<decimal> valor_contribuicao { get; set; }
    }
}
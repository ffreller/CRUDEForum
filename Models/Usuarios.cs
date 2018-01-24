using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDForum.Models
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage="Esse campo não pode ficar vazio.")]
        [MinLength(2,ErrorMessage="O campo 'Nome' precisa conter pelo menos dois caracteres.")]
        [MaxLength(10,ErrorMessage="O campo 'Nome' não pode conter mais que dez caracteres.")]
        public string nome { get; set; }
        [Display(Name="Login")]
        [Required(ErrorMessage="Esse campo não pode ficar vazio.")]
        [MinLength(5,ErrorMessage="O campo 'Login' precisa conter pelo menos cinco caracteres.")]
        [MaxLength(10,ErrorMessage="O campo 'Login' não pode conter mais que dezesseis caracteres.")]
        public string login { get; set; }
        [Display(Name="Senha")]
        [Required(ErrorMessage="Esse campo não pode ficar vazio.")]
        [MinLength(8,ErrorMessage="O campo 'Senha' precisa conter pelo menos oito caracteres.")]
        [MaxLength(16,ErrorMessage="O campo 'Senha' não pode conter mais que dezesseis caracteres.")]
        [RegularExpression(@"^[a-zA-Z-'/s]{1,40}$",ErrorMessage="Você não pode adiconar caracteres especiais")]
        public string senha { get; set; }
        public DateTime datacadastro { get; set; }

    }
}
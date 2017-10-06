using CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wakool.DataAnnotations;

namespace Entity
{
    [DisplayName("Clientes")]
    [IconAttribute("mdi-comment-account")]
    public class Cliente : EntityBase
    {

        [Required(ErrorMessage = "O nome é um campo obrigatório.")]
        [DisplayName("Nome")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,60}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        public string NomeUsuario { get; set; }

        [Required]
        [MaskedTextAttribute(Mask.CPF)]
        [StringLength(11, MinimumLength = 11, ErrorMessage =
           "O CPF deve ter exatamente 11 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O email é um campo obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage =
           "O email deve ter no mínimo 5 e no máximo 100 caracteres.")]
        public string Email { get; set; }

        [DisplayName("Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [DisplayName("Sexo")]
        public Enumerator Enum { get; set; }

        [DisplayName("Administrador")]
        public bool Admin { get; set; }
    }

    public enum Enumerator
    {
        Masculino, Feminino
    }
}

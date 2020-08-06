using System.ComponentModel.DataAnnotations; 

namespace AnimaDigital.Application.DTOs
{
    public abstract class BaseDTO
    {
        [Required(ErrorMessage = "Campo cpf é obrigatório!")]
        [MinLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        [MaxLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório!")]
        [MaxLength(150, ErrorMessage = "Campo email deve possuir no máximo 150 caracteres!")]
        [EmailAddress(ErrorMessage = "Campo email em formato incorreto!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        [MaxLength(150, ErrorMessage = "Campo nome deve possuir no máximo 150 caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo login é obrigatório!")]
        [MaxLength(20, ErrorMessage = "Campo login de possuir no máximo 20 caracteres!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo senha é obrigatório!")]
        [MaxLength(15, ErrorMessage = "Campo senha deve possuir no máximo 15 caracteres!")]
        public string Senha { get; set; }
    }
}

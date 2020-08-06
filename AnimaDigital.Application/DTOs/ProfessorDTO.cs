using System.ComponentModel.DataAnnotations;

namespace AnimaDigital.Application.DTOs
{
    public class ProfessorDTO : BaseDTO
    { 
        [Required(ErrorMessage = "Campo codFuncionario é obrigatório!")] 
        public int Codigo { get; set; }
    }
}

using System.ComponentModel.DataAnnotations; 

namespace AnimaDigital.Application.DTOs
{
    public class AlunoDTO : BaseDTO
    { 
        [Required(ErrorMessage = "Campo ra é obrigatório!")]
        //[MinLength(1, ErrorMessage = "Campo ra dever ser maior que 0!")] 
        public int Ra { get; set; }
    }
}

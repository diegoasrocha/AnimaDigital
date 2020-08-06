using System.ComponentModel.DataAnnotations; 

namespace AnimaDigital.Application.DTOs
{
    public class MatriculaDTO
    {
        [Required(ErrorMessage = "Campo codGrade é obrigatório!")]
        public int CodGrade { get; set; }

        [Required(ErrorMessage = "Campo ra é obrigatório!")] 
        public int Ra { get; set; }
    }
}

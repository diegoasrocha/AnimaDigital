using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations; 

namespace AnimaDigital.Application.DTOs
{
    public class GradeDTO
    { 
        [Required(ErrorMessage = "Campo codGrade é obrigatório!")]
        [JsonProperty(Order = 1)]
        public int CodGrade { get; set; }

        [Required(ErrorMessage = "Campo NomeCurso é obrigatório!")]
        [MaxLength(150, ErrorMessage = "Campo curso deve possuir no máximo 150 caracteres!")]
        [JsonProperty(Order = 4)]
        public string Curso { get; set; }

        [Required(ErrorMessage = "Campo NomeDisciplina é obrigatório!")]
        [MaxLength(100, ErrorMessage = "Campo disciplina deve possuir no máximo 10 caracteres!")]
        [JsonProperty(Order = 3)]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "Campo NomeTurma é obrigatório!")]
        [MaxLength(20, ErrorMessage = "Campo turma deve possuir no máximo 20 caracteres!")]
        [JsonProperty(Order = 2)]
        public string Turma { get; set; } 

        [Required(ErrorMessage = "Campo codFuncionario é obrigatorio!")]
        [JsonProperty(Order = 5)]
        public int CodFuncionario { get; set; }
    }
}

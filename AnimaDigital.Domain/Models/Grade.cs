using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimaDigital.Domain.Models
{
    [Table("Grade")]
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Campo codGrade é obrigatório!")]
        public int CodGrade { get; set; }

        [Required(ErrorMessage = "Campo NomeCurso é obrigatório!")]
        [MaxLength(150, ErrorMessage = "Campo curso deve possuir no máximo 150 caracteres!")]
        public string Curso { get; set; }

        [Required(ErrorMessage = "Campo NomeDisciplina é obrigatório!")]
        [MaxLength(100, ErrorMessage = "Campo disciplina deve possuir no máximo 10 caracteres!")]
        public string Disciplina { get; set; }

        [Required(ErrorMessage = "Campo NomeTurma é obrigatório!")]
        [MaxLength(20, ErrorMessage = "Campo turma deve possuir no máximo 20 caracteres!")]
        public string Turma { get; set; }

        [Required(ErrorMessage = "Campo cpf é obrigatório!")]
        [MinLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        [MaxLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo codFuncionario é obrigatorio!")] 
        public int CodFuncionario { get; set; } 

        public virtual Professor Professor { get; set; }

        public virtual ICollection<AlunoGrade> GradeAlunos { get; set; }

        public Grade() => this.GradeAlunos = new HashSet<AlunoGrade>();

        public Grade(int codGrade, string turma, string disciplina, string curso, int codFuncionario)
        {
            this.CodGrade = codGrade;
            this.Curso = curso;
            this.Disciplina = disciplina;
            this.Turma = turma;
            this.CodFuncionario = codFuncionario;
            this.GradeAlunos = new HashSet<AlunoGrade>();
        }
    }
}

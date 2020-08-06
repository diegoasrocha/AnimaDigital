using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimaDigital.Domain.Models
{
    [Table("Aluno")]
    public class Aluno
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Campo cpf é obrigatório!")]
        [MinLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        [MaxLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        [ForeignKey(nameof(Usuario))]
        public string Cpf { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Campo ra é obrigatório!")] 
        public int Ra { get; set; }


        public virtual ICollection<AlunoGrade> AlunoGrades { get; set; }
        public virtual Usuario Usuario { get; set; }


        public Aluno() => this.AlunoGrades = new HashSet<AlunoGrade>();

        public Aluno(string cpf, int ra)
        {
            this.Cpf = cpf;
            this.Ra = ra; 
            this.AlunoGrades = new HashSet<AlunoGrade>();
        }
    }
}

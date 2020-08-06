using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace AnimaDigital.Domain.Models
{
    [Table("AlunoGrade")]
    public class AlunoGrade
    {
        [Key]
        [Column(Order = 0)] 
        public string Cpf { get; set; }

        [Key]
        [Column(Order = 1)] 
        public int Ra { get; set; } 

        public virtual Aluno Aluno { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CodGrade { get; set; }
        public virtual Grade Grade{ get; set; }

        public AlunoGrade() { }

        public AlunoGrade(int codGrade, int ra)
        {
            this.Ra = ra;
            this.CodGrade = codGrade;
        }
    }
}

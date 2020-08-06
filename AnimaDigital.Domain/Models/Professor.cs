using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimaDigital.Domain.Models
{
    [Table("Professor")]
    public class Professor
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Campo cpf é obrigatório!")]
        [MinLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        [MaxLength(11, ErrorMessage = "Campo cpf deve possuir exatamente 11 caracteres!")]
        [ForeignKey(nameof(Usuario))]
        public string Cpf { get; private set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Campo codFuncionario é obrigatório!")] 
        public int CodFuncionario { get; set; }
        

        public virtual ICollection<Grade> Grades { get; set; }
        public virtual Usuario Usuario { get; set; }


        public Professor() => this.Grades = new HashSet<Grade>();

        public Professor(string cpf, int codFuncionario)
        {
            this.CodFuncionario = codFuncionario;
            this.Cpf = cpf;
            this.Grades = new HashSet<Grade>();
        }
    }
}

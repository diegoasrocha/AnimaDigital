using Newtonsoft.Json;
using System.Collections.Generic;

namespace AnimaDigital.Application.DTOs
{
    public class GradeProfessorAlunosDTO : GradeDTO
    {
        [JsonProperty(Order = 6)]
        public string NomeProfessor { get; set; }
        [JsonProperty(Order = 7)]
        public string CpfProfessor { get; set; }
        [JsonProperty(Order = 8)]
        public string EmailProfessor { get; set; }

        [JsonProperty(Order = 9)]
        public ICollection<AlunoGradeDTO> Alunos { get; set; }

        public GradeProfessorAlunosDTO()
        {
            Alunos = new HashSet<AlunoGradeDTO>();
        }
    }

    public class AlunoGradeDTO
    {
        [JsonProperty(Order = 10)]
        public string Nome { get; set; }
        [JsonProperty(Order = 11)]
        public int Ra { get; set; }
        [JsonProperty(Order = 12)]
        public string Email { get; set; }
    }
}

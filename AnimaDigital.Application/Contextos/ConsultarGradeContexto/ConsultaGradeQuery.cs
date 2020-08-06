using AnimaDigital.Application.DTOs;
using MediatR; 

namespace AnimaDigital.Application.Contextos.ConsultarGradeContexto
{
    public class ConsultaGradeQuery : IRequest<GradeProfessorAlunosDTO>
    {
        public int CodGrade { get; set; }

        public ConsultaGradeQuery() { }

        public ConsultaGradeQuery(int codGrade) => CodGrade = codGrade; 
    }
}

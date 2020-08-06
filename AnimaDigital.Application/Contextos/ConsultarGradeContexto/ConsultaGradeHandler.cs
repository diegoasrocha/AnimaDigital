using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.ConsultarGradeContexto
{
    public class ConsultaGradeHandler : IRequestHandler<ConsultaGradeQuery, GradeProfessorAlunosDTO>
    {
        private readonly IGradeRepository gradeRepository;

        public ConsultaGradeHandler(IGradeRepository gradeRepository)
            => this.gradeRepository = gradeRepository;  

        public Task<GradeProfessorAlunosDTO> Handle(ConsultaGradeQuery request, CancellationToken cancellationToken)
        {
            var codGrade = request.CodGrade;

            var grade = gradeRepository.GetByIdIncludeProfessorAlunos(codGrade);

            if(grade == null)
                throw new NaoEncontradoException("Grade não encontrada através do codGrade informado!");

            GradeProfessorAlunosDTO gradeDTO = new GradeProfessorAlunosDTO()
            {
                CodGrade = grade.CodGrade,
                Turma = grade.Turma,
                Disciplina = grade.Disciplina,
                Curso = grade.Curso,
                CodFuncionario = grade.CodFuncionario,
                NomeProfessor = grade.Professor.Usuario.Nome,
                CpfProfessor = grade.Professor.Cpf,
                EmailProfessor = grade.Professor.Usuario.Email,
                Alunos = grade.GradeAlunos.Select(ga => 
                    new AlunoGradeDTO { 
                        Nome = ga.Aluno.Usuario.Nome,
                        Email = ga.Aluno.Usuario.Email,
                        Ra = ga.Aluno.Ra
                    }).ToList()
            };

            return Task.FromResult(gradeDTO);
        }
    }
}

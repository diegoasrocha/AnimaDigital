using AnimaDigital.Application.Exceptions;
using AnimaDigital.Domain.Models;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.NovaMatriculaContexto
{
    public class NovaMatriculaHandler : IRequestHandler<NovaMatriculaCommand, bool>
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IAlunoRepository alunoRepository;

        public NovaMatriculaHandler(IGradeRepository gradeRepository, IAlunoRepository alunoRepository)
        {
            this.gradeRepository = gradeRepository;
            this.alunoRepository = alunoRepository;
        }

        public Task<bool> Handle(NovaMatriculaCommand request, CancellationToken cancellationToken)
        {
            var matricula = request.AlunoGrade;

            var grade = gradeRepository.GetByIdIncludeAlunos(matricula.CodGrade);

            if (grade == null)
                throw new NaoEncontradoException("Grade não encontrada através do codGrade informado!");

            if (grade.GradeAlunos.Where(ga => ga.Ra == matricula.Ra).Any())
                throw new ValidacaoException($"O aluno com RA {matricula.Ra} já está matriculado na grade {matricula.CodGrade}!");

            var subGrade = gradeRepository.GetSubGrades(grade).Where(g => g.GradeAlunos.Where(ga => ga.Ra == matricula.Ra).Any()).FirstOrDefault();

            if (subGrade != null)
                throw new ValidacaoException($"O aluno com RA {matricula.Ra} já está matriculado na grade {subGrade.CodGrade}, que é uma subgrade da grade {grade.CodGrade}!");

            var aluno = alunoRepository.GetByRaIncludeUsuario(matricula.Ra);

            if (aluno == null)
                throw new NaoEncontradoException("Aluno não encontrado através da RA informada!");

            matricula.Cpf = aluno.Cpf;

            if (grade.GradeAlunos.Count < 10)
            {
                grade.GradeAlunos.Add(matricula);
                gradeRepository.Update(grade);
            }
            else
            {
                subGrade = gradeRepository.GetSubGrade(grade);
                if(subGrade != null)
                {
                    subGrade.GradeAlunos.Add(matricula);
                    gradeRepository.Update(subGrade);
                }
                else
                {
                    var idGrade = gradeRepository.GetAll().Max(g => g.CodGrade) + 1;
                    var novaGrade = new Grade(idGrade, grade.Turma, grade.Disciplina, grade.Curso, grade.CodFuncionario) { Cpf = grade.Cpf };
                    novaGrade.GradeAlunos.Add(matricula);
                    gradeRepository.Add(novaGrade);
                }
            }

            return Task.FromResult(true);
        }
    }
}

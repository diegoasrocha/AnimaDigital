using AnimaDigital.Application.Exceptions;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.RemoverMatriculaContexto
{
    public class RemoverMatriculaHandler : IRequestHandler<RemoverMatriculaCommand, bool>
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IAlunoRepository alunoRepository;

        public RemoverMatriculaHandler(IGradeRepository gradeRepository, IAlunoRepository alunoRepository)
        {
            this.gradeRepository = gradeRepository;
            this.alunoRepository = alunoRepository;
        }

        public Task<bool> Handle(RemoverMatriculaCommand request, CancellationToken cancellationToken)
        { 
            var grade = gradeRepository.GetByIdIncludeAlunos(request.AlunoGrade.CodGrade);

            if (grade == null)
                throw new NaoEncontradoException("Grade não encontrada através do codGrade informado!");
             
            var aluno = alunoRepository.GetByRaIncludeUsuario(request.AlunoGrade.Ra);

            if (aluno == null)
                throw new NaoEncontradoException("Aluno não encontrado através da RA informada!");

            var matricula = grade.GradeAlunos.Where(g => g.Ra == aluno.Ra).FirstOrDefault();

            if (matricula != null)
            {
                grade.GradeAlunos.Remove(matricula);
                gradeRepository.Update(grade);
            }
            else
            {
                var subGrade = gradeRepository.GetSubGrades(grade).Where(g => g.GradeAlunos.Where(ga => ga.Ra == aluno.Ra).Any()).FirstOrDefault(); 

                if(subGrade != null)
                    matricula = subGrade.GradeAlunos.Where(g => g.Ra == aluno.Ra).FirstOrDefault();

                if (matricula == null)
                    throw new NaoEncontradoException("Não foi possivel remover a matricula. Matricula não encontrada!");
                else
                {
                    subGrade.GradeAlunos.Remove(matricula);
                    gradeRepository.Update(subGrade);
                }
            }

            return Task.FromResult(true);
        }
    }
}

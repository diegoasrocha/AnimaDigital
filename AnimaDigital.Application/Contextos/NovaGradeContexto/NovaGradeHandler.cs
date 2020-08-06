using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR; 
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.NovaGradeContexto
{
    public class NovaGradeHandler : IRequestHandler<NovaGradeCommand, GradeDTO>
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IProfessorRepository professorRepository;

        public NovaGradeHandler(IGradeRepository gradeRepository, IProfessorRepository professorRepository)
        {
            this.gradeRepository = gradeRepository;
            this.professorRepository = professorRepository;
        }

        public Task<GradeDTO> Handle(NovaGradeCommand request, CancellationToken cancellationToken)
        {
            var grade = request.Grade;

            var gradeBanco = gradeRepository.GetById(grade.CodGrade);

            if (gradeBanco != null)
                throw new ValidacaoException($"Já existe uma cadastrada com o codGrade {grade.CodGrade}!");

            var professor = professorRepository.GetByCodFuncionarioIncludeUsuario(grade.CodFuncionario);

            if (professor == null)
                throw new NaoEncontradoException("Professor não encontrado através do codFuncionario informado!");

            grade.Cpf = professor.Cpf;
            grade.CodFuncionario = professor.CodFuncionario;

            grade = gradeRepository.Add(grade);

            GradeDTO gradeDTO = new GradeDTO()
            {
                CodGrade = grade.CodGrade,
                Turma = grade.Turma,
                Disciplina = grade.Disciplina,
                Curso = grade.Curso,
                CodFuncionario = grade.CodFuncionario
            };

            return Task.FromResult(gradeDTO);
        }
    }
}

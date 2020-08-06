using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.ConsultarProfessorSalarioContexto
{
    public class ConsultaProfessorSalarioHandler : IRequestHandler<ConsultaProfessorSalarioQuery, ProfessorSalarioDTO>
    { 
        private readonly IUsuarioRepository usuarioRepository;

        public ConsultaProfessorSalarioHandler(IUsuarioRepository usuarioRepository)
            => this.usuarioRepository = usuarioRepository; 

        public Task<ProfessorSalarioDTO> Handle(ConsultaProfessorSalarioQuery request, CancellationToken cancellationToken)
        {
            var cpf = request.Cpf;

            var usuario = usuarioRepository.GetByCpfIncludeProfessor(cpf);

            if(usuario == null)
                throw new NaoEncontradoException("Professor não encontrado através do CPF informado!");

            if (usuario.Professor == null)
                throw new ValidacaoException("O CPF informado não pertence a um professor!");

            var totalGrades = usuario.Professor.Grades.Count;
            var totalAlunos = usuario.Professor.Grades.Sum(g => g.GradeAlunos.Count);

            ProfessorSalarioDTO professorDTO = new ProfessorSalarioDTO()
            {
                CodFuncionario = usuario.Professor.CodFuncionario,
                Nome = usuario.Nome,
                Cpf = usuario.Professor.Cpf,
                Email = usuario.Email,
                TotalGrades = totalGrades,
                TotalAlunos = totalAlunos,
                Salario = CalcularSalarioProfessor.CalcularSalario(qtdAlunos: totalAlunos, qtdGrades: totalGrades)
            };

            return Task.FromResult(professorDTO); 
        }
    }
}

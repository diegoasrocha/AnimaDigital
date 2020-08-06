using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.NovoProfessorContexto
{
    public class NovoProfessorHandler : IRequestHandler<NovoProfessorCommand, ProfessorDTO>
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IProfessorRepository professorRepository;

        public NovoProfessorHandler(IUsuarioRepository usuarioRepository, IProfessorRepository professorRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.professorRepository = professorRepository;
        }

        public Task<ProfessorDTO> Handle(NovoProfessorCommand request, CancellationToken cancellationToken)
        {
            var usuario = request.Usuario;

            if (usuarioRepository.UsuarioExiste(usuario) || professorRepository.ProfessorExiste(usuario.Professor))
            {
                throw new ConflitException("Já existe um usuário/professor cadastrado com o CPF, email, login e/ou CodFuncionario informado(s)!");
            }

            usuario = usuarioRepository.Add(usuario);

            ProfessorDTO professor = new ProfessorDTO()
            {
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Login = usuario.Login,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Codigo = usuario.Professor.CodFuncionario
            };

            return Task.FromResult(professor);
        }
    }
}

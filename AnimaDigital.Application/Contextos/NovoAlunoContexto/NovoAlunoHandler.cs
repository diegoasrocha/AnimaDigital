using AnimaDigital.Application.DTOs;
using AnimaDigital.Application.Exceptions;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using MediatR; 
using System.Threading;
using System.Threading.Tasks;

namespace AnimaDigital.Application.Contextos.NovoAlunoContexto
{
    public class NovoAlunoHandler : IRequestHandler<NovoAlunoCommand, AlunoDTO>
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IAlunoRepository alunoRepository;

        public NovoAlunoHandler(IUsuarioRepository usuarioRepository, IAlunoRepository alunoRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.alunoRepository = alunoRepository;
        }
         
        public Task<AlunoDTO> Handle(NovoAlunoCommand request, CancellationToken cancellationToken)
        {
            var usuario = request.Usuario;

            if(usuarioRepository.UsuarioExiste(usuario) || alunoRepository.AlunoExiste(usuario.Aluno))
            {
                throw new ConflitException("Já existe um usuário/aluno cadastrado com o CPF, email, login e/ou RA informado(s)!");
            }

            usuario = usuarioRepository.Add(usuario);

            AlunoDTO aluno = new AlunoDTO()
            {
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Login = usuario.Login,
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Ra = usuario.Aluno.Ra
            };

            return Task.FromResult(aluno);
        }
    }
}

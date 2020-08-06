using AnimaDigital.Application.DTOs;
using AnimaDigital.Domain.Models;
using MediatR; 

namespace AnimaDigital.Application.Contextos.NovoAlunoContexto
{
    public class NovoAlunoCommand : IRequest<AlunoDTO>
    {
        public Usuario Usuario { get; set; }
    }
}

using AnimaDigital.Application.DTOs;
using AnimaDigital.Domain.Models;
using MediatR; 

namespace AnimaDigital.Application.Contextos.NovoProfessorContexto
{
    public class NovoProfessorCommand : IRequest<ProfessorDTO>
    {
        public Usuario Usuario { get; set; }
    }
}

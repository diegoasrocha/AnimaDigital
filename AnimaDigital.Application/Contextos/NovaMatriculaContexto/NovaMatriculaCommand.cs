using AnimaDigital.Domain.Models;
using MediatR; 

namespace AnimaDigital.Application.Contextos.NovaMatriculaContexto
{
    public class NovaMatriculaCommand : IRequest<bool>
    {
        public AlunoGrade AlunoGrade { get; set; }
    }
}

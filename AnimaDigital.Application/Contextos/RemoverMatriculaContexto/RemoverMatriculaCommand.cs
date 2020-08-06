using AnimaDigital.Domain.Models;
using MediatR; 

namespace AnimaDigital.Application.Contextos.RemoverMatriculaContexto
{
    public class RemoverMatriculaCommand : IRequest<bool>
    {
        public AlunoGrade AlunoGrade { get; set; }
    }
}

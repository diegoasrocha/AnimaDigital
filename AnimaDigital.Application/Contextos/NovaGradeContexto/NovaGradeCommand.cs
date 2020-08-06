using AnimaDigital.Application.DTOs;
using AnimaDigital.Domain.Models;
using MediatR; 

namespace AnimaDigital.Application.Contextos.NovaGradeContexto
{
    public class NovaGradeCommand : IRequest<GradeDTO>
    {
        public Grade Grade { get; set; }
    }
}

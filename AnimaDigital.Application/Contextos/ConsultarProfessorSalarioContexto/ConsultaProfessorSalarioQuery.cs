using AnimaDigital.Application.DTOs;
using MediatR; 

namespace AnimaDigital.Application.Contextos.ConsultarProfessorSalarioContexto
{
    public class ConsultaProfessorSalarioQuery : IRequest<ProfessorSalarioDTO>
    {
        public string Cpf { get; set; }

        public ConsultaProfessorSalarioQuery(string cpf) => Cpf = cpf; 
    }
}

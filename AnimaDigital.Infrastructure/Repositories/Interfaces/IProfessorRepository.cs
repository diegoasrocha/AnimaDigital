using AnimaDigital.Domain.Models;  

namespace AnimaDigital.Infrastructure.Repositories.Interfaces
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Professor GetByCodFuncionarioIncludeUsuario(int codFuncionario);
        bool ProfessorExiste(Professor professor);
    }
}

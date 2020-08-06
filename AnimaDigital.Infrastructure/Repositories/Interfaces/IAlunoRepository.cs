using AnimaDigital.Domain.Models;  

namespace AnimaDigital.Infrastructure.Repositories.Interfaces
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Aluno GetByRaIncludeUsuario(int ra);  
        bool AlunoExiste(Aluno aluno);
    }
}

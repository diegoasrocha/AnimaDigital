using AnimaDigital.Domain.Models;
using AnimaDigital.Infrastructure.DBConfiguration;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore; 
using System.Linq;

namespace AnimaDigital.Infrastructure.Repositories
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AnimaContext dbContext) : base(dbContext) { }

        public Aluno GetByRaIncludeUsuario(int ra)
            => dbSet.Include(a => a.Usuario).Where(a => a.Ra == ra).FirstOrDefault();

        public bool AlunoExiste(Aluno aluno)
            => dbSet.Where(a => a.Cpf == aluno.Cpf || a.Ra == aluno.Ra).ToList().Any(); 
    }
}

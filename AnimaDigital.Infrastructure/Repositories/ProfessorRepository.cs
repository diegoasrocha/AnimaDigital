using AnimaDigital.Domain.Models;
using AnimaDigital.Infrastructure.DBConfiguration;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;  
using System.Linq;  

namespace AnimaDigital.Infrastructure.Repositories
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(AnimaContext dbContext) : base(dbContext) { }

        public Professor GetByCodFuncionarioIncludeUsuario(int codFuncionario)
            => dbSet.Include(p => p.Usuario).Where(p => p.CodFuncionario == codFuncionario).FirstOrDefault(); 

        public bool ProfessorExiste(Professor professor)
            => dbSet.Where(p => p.Cpf == professor.Cpf || p.CodFuncionario == professor.CodFuncionario).ToList().Any();
    }
}

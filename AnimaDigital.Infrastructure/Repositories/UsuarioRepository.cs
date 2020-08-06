using AnimaDigital.Domain.Models;
using AnimaDigital.Infrastructure.DBConfiguration;
using AnimaDigital.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;  
using System.Linq; 

namespace AnimaDigital.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AnimaContext dbContext) : base(dbContext) { }

        public Usuario GetByCpfIncludeProfessor(string cpf)
            => dbSet.Include(u => u.Professor).ThenInclude(p => p.Grades).ThenInclude(g => g.GradeAlunos)
                    .Where(u => u.Cpf == cpf).FirstOrDefault(); 

        public bool UsuarioExiste(Usuario usuario)
            => dbSet.Where(u => u.Cpf == usuario.Cpf || u.Email == usuario.Email || u.Login == usuario.Login ).ToList().Any();
    }
}

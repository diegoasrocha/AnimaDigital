using AnimaDigital.Domain.Models;  

namespace AnimaDigital.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>  
    {
        Usuario GetByCpfIncludeProfessor(string cpf);
        bool UsuarioExiste(Usuario usuario);
    }
}

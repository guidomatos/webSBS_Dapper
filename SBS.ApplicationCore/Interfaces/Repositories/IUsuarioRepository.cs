using SBS.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBS.ApplicationCore.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> BuscarUsuario();
    }
}

using SBS.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBS.ApplicationCore.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> BuscarUsuario();
    }
}

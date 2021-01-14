using SBS.ApplicationCore.DTO;
using SBS.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBS.ApplicationCore.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<BusquedaUsuarioDto>> BuscarUsuario(FiltroBusquedaUsuarioDto param);
        Task<int> GrabarUsuario(Usuario usuario);
        Task<int> EliminarUsuario(int usuarioId);
    }
}

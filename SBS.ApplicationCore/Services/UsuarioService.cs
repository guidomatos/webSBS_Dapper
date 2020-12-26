using SBS.ApplicationCore.Entities;
using SBS.ApplicationCore.Interfaces.Repositories;
using SBS.ApplicationCore.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SBS.ApplicationCore.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable<Usuario>> BuscarUsuario()
        {
            return await _usuarioRepository.BuscarUsuario();
        }
    }
}

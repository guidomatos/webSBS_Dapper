using Dapper;
using Microsoft.Extensions.Configuration;
using SBS.ApplicationCore.Entities;
using SBS.ApplicationCore.Interfaces.Repositories;
using SBS.Infrastructure.Connection.Dapper;
using SBS.Infrastructure.Connection.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SBS.Infrastructure.Repositories
{
    public class UsuarioRepository: BaseDapper, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration)
            : base(configuration)
        {
        }
        public async Task<IEnumerable<Usuario>> BuscarUsuario()
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    var result = await dbConnection.QueryAsync<Usuario>(
                        "SELECT*FROM Usuario",
                        new
                        {

                        },
                        commandType: CommandType.Text
                    );

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
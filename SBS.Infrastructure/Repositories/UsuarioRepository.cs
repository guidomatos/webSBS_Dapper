using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SBS.ApplicationCore.DTO;
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
        public async Task<IEnumerable<BusquedaUsuarioDto>> BuscarUsuario(FiltroBusquedaUsuarioDto param)
        {
            try
            {
                using (IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ROLID", param.RolId);
                    parameters.Add("P_CODIGOUSUARIO", param.CodigoUsuario);
                    parameters.Add("IO_CURSOR", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);


                    var result = await dbConnection.QueryAsync<BusquedaUsuarioDto>(
                        "SBS_BUSCAR_USUARIO",
                        parameters,
                        commandType: CommandType.StoredProcedure
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
        public async Task<int> InsertarUsuario(Usuario usuario)
        {
            int result = 0;
            try
            {
                using (IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    var parameters = new OracleDynamicParameters();
                    parameters.Add("P_ROLID", usuario.RolId);
                    parameters.Add("P_CODIGOUSUARIO", usuario.CodigoUsuario);
                    parameters.Add("P_CLAVESECRETA", usuario.ClaveSecreta);
                    parameters.Add("P_EMAIL", usuario.Email);
                    parameters.Add("P_APELLIDOPATERNO", usuario.ApellidoPaterno);
                    parameters.Add("P_APELLIDOMATERNO", usuario.ApellidoMaterno);
                    parameters.Add("P_PRIMERNOMBRE", usuario.PrimerNombre);
                    parameters.Add("P_SEGUNDONOMBRE", usuario.SegundoNombre);
                    parameters.Add("P_ALIAS", usuario.Alias);
                    parameters.Add("P_RESULT", dbType: OracleDbType.Decimal, direction: ParameterDirection.Output);


                    await dbConnection.ExecuteAsync(
                        "SBS_INSERTAR_USUARIO",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    result = Convert.ToInt32(parameters.Get<OracleDecimal>("P_RESULT").Value);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public async Task<int> ModificarUsuario(Usuario usuario)
        {
            int result = 0;
            try
            {
                using (IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    result = await dbConnection.ExecuteScalarAsync<int>(
                        "ModificarUsuario",
                        new
                        {
                            usuario.UsuarioId,
                            usuario.RolId,
                            usuario.Email,
                            usuario.ApellidoPaterno,
                            usuario.ApellidoMaterno,
                            usuario.PrimerNombre,
                            usuario.SegundoNombre,
                            usuario.Alias
                        },
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public async Task<int> EliminarUsuario(int usuarioId)
        {
            int result = 0;
            try
            {
                using (IDbConnection dbConnection = ConnectionManager.GetConnection())
                {
                    dbConnection.Open();

                    result = await dbConnection.ExecuteScalarAsync<int>(
                        "EliminarUsuario",
                        new
                        {
                            UsuarioId = usuarioId
                        },
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

    }
}
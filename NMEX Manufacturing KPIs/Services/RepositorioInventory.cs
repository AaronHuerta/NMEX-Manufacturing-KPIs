using Dapper;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models;

namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioInventory
    {
        Task<IEnumerable<Persona>> Obtener();
    }

    public class RepositorioInventory: IRepositorioInventory
    {
        private readonly string connectionString;

        public RepositorioInventory(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        //Método para obtener el listado de personas
        public async Task<IEnumerable<Persona>> Obtener()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Persona>(@"SELECT*
                                                          FROM Persona");
        }


    }
}

using Dapper;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;

namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioInventory
    {
        Task CreateLocation(Location location);
        Task<IEnumerable<Location>> GetLocations();
        Task<IEnumerable<Plant>> GetPlants();
    }

    public class RepositorioInventory : IRepositorioInventory
    {
        private readonly string connectionString;

        public RepositorioInventory(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        //Método para obtener el listado locaciones
        public async Task<IEnumerable<Location>> GetLocations()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Location>(@"SELECT Location_id, Location_description, L.Plant_id, L.Active, P.Plant_description AS Plant
                                                            FROM Location AS L inner join Plant AS P 
                                                            ON L.Plant_id = P.Plant_id 
                                                            WHERE L.Active=1");
        }

        //Método para crear un nuevo registro
        public async Task CreateLocation(Location location)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Location(Location_description, Plant_id, Active)
                                                              VALUES (@Location_description, @Plant_id, 1)
                                                              SELECT SCOPE_IDENTITY();", location);

            location.Location_id = id;
        }


        //Método para obtener el listado de plantas
        public async Task<IEnumerable<Plant>> GetPlants()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Plant>(@"SELECT Plant_id, Plant_description, Active
                                                          FROM Plant
                                                          WHERE Active=1");
        }


    }
}

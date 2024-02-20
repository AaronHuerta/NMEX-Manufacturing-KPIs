using Dapper;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;

namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioInventory
    {
        Task CreateLocation(Location location);
        Task CreateModel(Model model);
        Task DeleteLocation(int Location_Id);
        Task DeleteModel(int Model_id);
        Task EditLocation(Location location);
        Task EditModel(Model model);
        Task<Location> GetByIdLocation(int Location_id);
        Task<Model> GetByIdModel(int Model_id);
        Task<IEnumerable<Location>> GetLocations();
        Task<IEnumerable<Model>> GetModels();
        Task<IEnumerable<Plant>> GetPlants();
    }

    public class RepositorioInventory : IRepositorioInventory
    {
        private readonly string connectionString;

        public RepositorioInventory(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Métodos queries del modulo de Locations ------------------------------------------------------------------------------------------------------------------------------

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


        //Método para seleccionar ID
        public async Task<Location> GetByIdLocation(int Location_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Location>(@"SELECT Location_id, Location_description, L.Plant_id, L.Active, P.Plant_description AS Plant
                                                                        FROM Location AS L inner join Plant AS P 
                                                                        ON L.Plant_id = P.Plant_id 
                                                                        WHERE L.Active=1 and Location_id = @Location_id", new {Location_id});

         
        }


        //Método para editar registro
        public async Task EditLocation(Location location)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Location
                                                SET Location_description = @Location_description, Plant_id=@Plant_id,Active='1'
                                                WHERE Location_id = @Location_id", location);

            
        }


        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteLocation(int Location_Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Location
                                                SET Active='0'
                                                WHERE Location_id = @Location_id", new {Location_Id});


        }


        //Métodos queries del modulo de Models ------------------------------------------------------------------------------------------------------------------------------

        //Método para obtener el listado models
        public async Task<IEnumerable<Model>> GetModels()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Model>(@"SELECT Model_id, Model_description, Active
                                                        FROM Model
                                                        Where Active = '1'");
        }

        //Método para crear un nuevo registro
        public async Task CreateModel(Model model)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Model (Model_description, Active)
                                                              VALUES (@Model_description, '1')
                                                              SELECT SCOPE_IDENTITY();", model);

            model.Model_id = id;
        }

        //Método para seleccionar ID
        public async Task<Model> GetByIdModel(int Model_id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Model>(@"SELECT Model_id, Model_description, Active
                                                                    FROM Model
                                                                    Where Active = '1' and Model_id=@Model_id", new { Model_id });
        }



        //Método para editar registro
        public async Task EditModel(Model model)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Model
                                                SET Model_description = @Model_description, Active = '1'
                                                WHERE Model_id = @Model_id;", model);


        }



        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteModel(int Model_id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Model
                                            SET Active='0'
                                            WHERE Model_id = @Model_id", new { Model_id });
        }


        //Métodos queries del modulo de Plants ------------------------------------------------------------------------------------------------------------------------------
        //Método para obtener el listado de plantas
        public async Task<IEnumerable<Plant>> GetPlants()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Plant>(@"SELECT Plant_id, Plant_description, Active
                                                        FROM Plant
                                                        WHERE Active='1'");
        }


    }
}

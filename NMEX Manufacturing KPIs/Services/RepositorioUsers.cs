using Dapper;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Models.Module_User;

namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioUsers
    {
        Task<int> CreateUser(Users user);
        Task<IEnumerable<Plant>> GetPlants();
        Task<IEnumerable<Users>> GetUsersRecords();
        Task<Users> SearchUserByEmail(string EmailNormalized);
        Task<Users> SearchUserByIdUserNissan(string IdUserNissan);
    }

    public class RepositorioUsers : IRepositorioUsers
    {
        private readonly string connectionString;

        public RepositorioUsers(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Método para obtener el listado de inventory
        public async Task<IEnumerable<Users>> GetUsersRecords()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Users>(@"SELECT U.FirstName, U.LastNameMaternal, U.LastNamePaternal, U.Email, U.Password, U.Privilege, U.Active, P.Plant_description as Plant, U.IdUserNissan
                                                        FROM Users AS U inner join Plant AS P 
                                                        ON U.Plant_id = P.Plant_id;");
        }
        public async Task<int> CreateUser(Users user)
        {
            //usuario.EmailNormalizado=usuario.Email.ToUpper();
            using var connection = new SqlConnection(connectionString);
            var user_id = await connection.QuerySingleAsync<int>(@"INSERT INTO Users (FirstName, LastNamePaternal, LastNameMaternal, Email, EmailNormalized, Password, Privilege, Active,IdUserNissan, Plant_id)
                                                                    VALUES (@FirstName, @LastNamePaternal, @LastNameMaternal, @Email, @EmailNormalized, @Password,@Privilege, @Active,@IdUserNissan, @Plant_id)
                                                                    SELECT SCOPE_IDENTITY();", user);
            return user_id;
        }

        public async Task<Users> SearchUserByEmail(string EmailNormalized)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Users>(@"SELECT*
                                                                        FROM Users
                                                                        WHERE EmailNormalized = @EmailNormalized;", new { EmailNormalized });
        } 
        
        
        public async Task<Users> SearchUserByIdUserNissan(string IdUserNissan)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Users>(@"SELECT*
                                                                        FROM Users
                                                                        WHERE IdUserNissan = @IdUserNissan;", new { IdUserNissan });
        }

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

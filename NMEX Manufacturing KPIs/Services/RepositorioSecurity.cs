using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models;
using NMEX_Manufacturing_KPIs.Models.Module_Security;

namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioSecurity
    {
        Task<IEnumerable<Function>> GetFunctions();
        Task<IEnumerable<Categories>> Obtener();
        Task<IEnumerable<SubCategory>> GetByIdCategory(int Category_id);

        Task CreateCategory(Categories category);
        Task DeleteCategory(int Category_Id);
        Task <Categories> GetDeleteByIdCategory(int Category_id);

        
        Task CreateSubCategory(SubCategory category);
        Task DeleteSubCategory(int SubCategory_id);
        Task<SubCategory> GetDeleteByIdSubCategory(int SubCategory_id);
       
        Task UpdateCalification(int subCategoryId, string newCalification, string comment);


    }
    public class RepositorioSecurity: IRepositorioSecurity
    {
        private readonly string connectionString;

        public RepositorioSecurity(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Método para obtener el listado de Security
        public async Task<IEnumerable<Categories>> Obtener()

        {

            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Categories>(@"EXEC[dbo].[ShowFunctionResult]
        @Plan_idFilter = 1");

        }
        
        //Método para crear un nuevo registro
        public async Task CreateCategory(Categories category)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Categories (Category_name, Category_description, Category_identifier, Function_id, Active) 
                                                            VALUES (@Category_name, @Category_description, @Category_identifier, @Function_id ,1); 
                                                            SELECT SCOPE_IDENTITY();", category);

            category.Category_id = id;
        }

        public async Task CreateSubCategory(SubCategory category)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"  INSERT INTO [Nissan].[dbo].[SubCategory] (SubCategory_description, Category_id, Active) 
                                                            VALUES (@SubCategory_description, @Category_id,1);
                                                            EXEC [dbo].[InsertTableSecurityByPlant] @Plan_idFilter = 1, @Category_idFilter = @Category_id
                                                            SELECT SCOPE_IDENTITY();", category);

            category.Category_id = id;
        }





        //Método para obtener el Category_id de Categories y al dar click en una categoria poder pasar a SubCategoryModel con base al id
        public async Task<IEnumerable<SubCategory>> GetByIdCategory(int Category_id)

        {

            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<SubCategory>(@"EXEC [dbo].[InsertTableSecurityByPlant]  
	@Plan_idFilter = 1 ,@Category_idFilter = @Category_id", new { Category_id });
        }

        public async Task<Categories> GetDeleteByIdCategory(int Category_id)

        {

            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Categories>(@"SELECT Category_id, Category_description, Category_identifier, C.Function_id, C.Active, F.Function_description AS Function_name
                    FROM [Nissan].[dbo].[Categories] AS C
                    INNER JOIN [Nissan].[dbo].[Function] AS F ON C.Function_id = F.Function_id
                    WHERE C.Active = 1 AND F.ACTIVE = 1 AND Category_id = @Category_id", new { Category_id });

        }

        //Método para actualizar el active a 0 (Borrado Logico)
        public async Task DeleteCategory(int Category_Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE [Nissan].[dbo].[Categories]
                                                SET Active='0'
                                                WHERE Category_Id = @Category_Id", new { Category_Id });


        }

        public async Task<SubCategory> GetDeleteByIdSubCategory(int SubCategory_id)

        {

            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<SubCategory>(@" SELECT
    S.[Security_id],
    SC.[SubCategory_id]
FROM 
    [Nissan].[dbo].[SubCategory] AS SC
    INNER JOIN 
    [Nissan].[dbo].[Security] S ON SC.[SubCategory_id] = S.[SubCategory_id] AND S.[Plant_id] = 1
    INNER JOIN
    [Nissan].[dbo].[Categories] C ON SC.[Category_id] = C.[Category_id]
WHERE 
    C.Active = 1 AND S.Active = 1 AND SC.Active = 1
    AND S.[SubCategory_id] = @SubCategory_id", new { SubCategory_id });

        }

        public async Task DeleteSubCategory(int SubCategory_id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE [Nissan].[dbo].[Security]
                                            SET Active = 0
                                            WHERE[SubCategory_id] = @SubCategory_id
	                                        UPDATE [Nissan].[dbo].[SubCategory]
                                            SET Active = 0
                                            WHERE [SubCategory_id] = @SubCategory_id", new { SubCategory_id });

        }

        public async Task UpdateCalification(int subCategoryId, string newCalification, string comment)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "  UPDATE Security SET [Result] = @newCalification, Comment = @Comment where Plant_id = 1 and SubCategory_id = @subCategoryId",
                new { NewCalification = newCalification, SubCategoryId = subCategoryId, Comment = comment }
            );
        }


        //Método para obtener el listado de Function
        public async Task<IEnumerable<Function>> GetFunctions()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Function>(@"  SELECT [Function_id], [Function_description], Active
                                                        FROM [Nissan].[dbo].[Function]
                                                        WHERE Active='1'");
        }

    }
}

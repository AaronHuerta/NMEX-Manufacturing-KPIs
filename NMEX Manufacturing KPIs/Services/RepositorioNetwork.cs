using Dapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NMEX_Manufacturing_KPIs.Models;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Models.Module_Network;
using NMEX_Manufacturing_KPIs.Services;


namespace NMEX_Manufacturing_KPIs.Services
{

    public interface IRepositorioNetwork
    {
        Task InsertDevice(LanSwitchIssues LanSwitchIssues);
        Task InsertEndpoints(LanSwitchEndpoint LanSwitchEndpoints);

        Task CreateNetworkRecord(NetworkCreationIssue worksheets);

        Task CreateNetworkRecord(NetworkCreationDevice NetworkRecordsDevice);

        Task CreateNetworkRecord(NetworkCreationEndpoint NetworkRecordsEndpoint);

        Task CreateNetworkRecord(NetworkCreationWLC NetworkRecordsWLC);
    }
    public class RepositorioNetwork : IRepositorioNetwork
    {
        private readonly string connectionString;

        public RepositorioNetwork(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InsertDevice(LanSwitchIssues LanSwitchIssues)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO [Nissan].[dbo].[LanSwitchDevice]
                                                              ([Device]) VALUES(@Device)", LanSwitchIssues);
        }

        public async Task InsertEndpoints(LanSwitchEndpoint LanSwitchEndpoints)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO [Nissan].[dbo].[LanSwitchDevice]
                                                              ([Device]) VALUES(@Device)", LanSwitchEndpoints);
        }

        public async Task CreateNetworkRecord(NetworkCreationIssue NetworkRecordsIssues)
        {
            using var connection = new SqlConnection(connectionString);
            var parameters = new
            {
                Device = NetworkRecordsIssues.Device,
                Type_affectation = NetworkRecordsIssues.Type_affectation,
                Description_affectation = NetworkRecordsIssues.Description_affectation,
                Suggestion = NetworkRecordsIssues.Suggestion,
                Criticality_level = NetworkRecordsIssues.Criticality_level
            };
            var RecordsIssues = await connection.QuerySingleAsync<int>(@"INSERT INTO [Nissan].[dbo].[LanSwitchIssues] ([Device]
                                                    ,[Type_affectation]
                                                    ,[Description_affectation]
                                                    ,[Suggestion]
                                                    ,[Criticality_level]
                                                    ,[LanSwitchIssues_data]
                                                    ,[Plant_id]) 
                                                    VALUES 
                                                    (@Device, @Type_affectation, 
                                                    @Description_affectation, 
                                                    @Suggestion,    
                                                    @Criticality_level,
                                                    GETDATE()
                                                    , 1);
                                                    SELECT SCOPE_IDENTITY();", parameters);
        }



        public async Task CreateNetworkRecord(NetworkCreationDevice NetworkRecordsDevice)
        {
            using var connection = new SqlConnection(connectionString);
            var parameters = new
            {
                Device = NetworkRecordsDevice.Device,
                CPU_processing = NetworkRecordsDevice.CPU_processing,
                Temperature_level = NetworkRecordsDevice.Temperature_level,
                FAN_status = NetworkRecordsDevice.FAN_status,
                Bandwidth = NetworkRecordsDevice.Bandwidth,
                Power_source = NetworkRecordsDevice.Power_source
            };
            var RecordsDevice = await connection.QuerySingleAsync<int>(@"INSERT INTO [Nissan].[dbo].[LanSwitchDevice] ([Device]
                                                                          ,[CPU_processing]
                                                                          ,[Temperature_level]
                                                                          ,[FAN_status]
                                                                          ,[Bandwidth]
                                                                          ,[Power_source]
	                                                                      ,[LanSwitchDevice_data]
                                                                          ,[Plant_id]) 
                                                                           VALUES 
                                (@Device, @CPU_processing,@Temperature_level,@FAN_status,@Bandwidth,@Power_source,GETDATE(), 1); 
                                SELECT SCOPE_IDENTITY();", parameters);
        }

        public async Task CreateNetworkRecord(NetworkCreationEndpoint NetworkRecordsEndpoint)
        {
            using var connection = new SqlConnection(connectionString);
            var parameters = new
            {
                Device = NetworkRecordsEndpoint.Device,
                Port = NetworkRecordsEndpoint.Port,
                Description_port = NetworkRecordsEndpoint.Description_port,
                Category_issue = NetworkRecordsEndpoint.Category_issue,
                Description_issue = NetworkRecordsEndpoint.Description_issue,
            };
            var RecordsDevice = await connection.QuerySingleAsync<int>(@"INSERT INTO [Nissan].[dbo].[LanSwitchEndpoints] ([Device]
                                                                          ,[Port]
                                                                          ,[Description_port]
                                                                          ,[Category_issue]
                                                                          ,[Description_issue]
	                                                                      ,[LanSwitchEndpoints_data]
                                                                          ,[Plant_id]) 
                VALUES (@Device, @Port, @Description_port, @Category_issue, @Description_issue,GETDATE() , 1); 
                                SELECT SCOPE_IDENTITY();", parameters);
        }
        public async Task CreateNetworkRecord(NetworkCreationWLC NetworkRecordsWLC)
        {
            using var connection = new SqlConnection(connectionString);
            var parameters = new
            {
                Acces_Point = NetworkRecordsWLC.Acces_Point,
                Number_connected_devices_2_4 = NetworkRecordsWLC.Number_connected_devices_2_4,
                Number_connected_devices_5 = NetworkRecordsWLC.Number_connected_devices_5,
                Canal_2_4 = NetworkRecordsWLC.Canal_2_4,
                Canal_5 = NetworkRecordsWLC.Canal_5,
                Frecuency_2_4 = NetworkRecordsWLC.Frecuency_2_4,
                Frecuency_5 = NetworkRecordsWLC.Frecuency_5,
                Number_SSID_propagated = NetworkRecordsWLC.Number_SSID_propagated,
            };
            var RecordsDevice = await connection.QuerySingleAsync<int>(@"INSERT INTO [Nissan].[dbo].[WLC] ([Acces_Point]
                                                                          ,[Number_connected_devices_2_4]
                                                                          ,[Number_connected_devices_5]
                                                                          ,[Canal_2_4]
                                                                          ,[Canal_5]
                                                                          ,[Frecuency_2_4]
                                                                          ,[Frecuency_5]
                                                                          ,[Number_SSID_propagated]
                                                                          ,[WLC_data]
                                                                          ,[Plant_id]) 
                                                                           VALUES (@Acces_Point, @Number_connected_devices_2_4, @Number_connected_devices_5, @Canal_2_4, @Canal_5, @Frecuency_2_4, @Frecuency_5, @Number_SSID_propagated,GETDATE(),1); 
                                SELECT SCOPE_IDENTITY();", parameters);
        }
    }
}

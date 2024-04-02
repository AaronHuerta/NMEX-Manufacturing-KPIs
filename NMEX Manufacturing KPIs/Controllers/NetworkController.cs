using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Models.Module_Network;

//using NMEX_Manufacturing_KPIs.Models.Module_Network;
using NMEX_Manufacturing_KPIs.Services;

namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class NetworkController : Controller
    {

        private readonly IRepositorioNetwork repositorioNetwork;
        private readonly IMapper mapper;


        public NetworkController(IRepositorioNetwork repositorioNetwork, IMapper mapper)
        {
            this.repositorioNetwork = repositorioNetwork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InventoryRecordExcel(IFormFile excelFile)
        {
            try
            {
                if (excelFile == null || excelFile.Length == 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheets = new Dictionary<int, IXLWorksheet>
                        {
                        {1, workbook.Worksheet(1) },
                        {2, workbook.Worksheet(2) },
                        {3, workbook.Worksheet(3) },
                        {4, workbook.Worksheet(4) }
                        };

                        foreach (var worksheet in worksheets.Values)
                        {
                            var firstRowUsed = worksheet.FirstRowUsed().RowNumber();
                            var lastRowUsed = worksheet.LastRowUsed().RowNumber();
                            //var lastRowUsed = worksheet.RowsUsed().Count();


                            for (int rowNum = 2; rowNum <= lastRowUsed; rowNum++)
                            {
                                var LanSwitchIssues = new LanSwitchIssues();
                                var LanSwitchDevice = new LanSwitchDevice();
                                var LanSwitchEndpoint = new LanSwitchEndpoint();
                                var WLC = new WLC();

                                if (worksheet.Name == "SWITCH ISSUES")
                                {
                                    LanSwitchIssues.Device = (string)worksheet.Cell(rowNum, 2).Value;
                                    LanSwitchIssues.Type_affectation = (string)worksheet.Cell(rowNum, 3).Value;
                                    LanSwitchIssues.Description_affectation = (string)worksheet.Cell(rowNum, 4).Value;
                                    LanSwitchIssues.Suggestion = (string)worksheet.Cell(rowNum, 5).Value;
                                    LanSwitchIssues.Criticality_level = (string)worksheet.Cell(rowNum, 6).Value;

                                    var NetworkRecordsIssues = new NetworkCreationIssue();
                                    NetworkRecordsIssues.Device = LanSwitchIssues.Device;
                                    NetworkRecordsIssues.Type_affectation = LanSwitchIssues.Type_affectation;
                                    NetworkRecordsIssues.Description_affectation = LanSwitchIssues.Description_affectation;
                                    NetworkRecordsIssues.Suggestion = LanSwitchIssues.Suggestion;
                                    NetworkRecordsIssues.Criticality_level = LanSwitchIssues.Criticality_level;
                                    await repositorioNetwork.CreateNetworkRecord(NetworkRecordsIssues);
                                }
                                if (worksheet.Name == "SWITCH DEVICE")
                                {
                                    LanSwitchDevice.Device = (string)worksheet.Cell(rowNum, 2).Value;
                                    LanSwitchDevice.CPU_processing = (int)worksheet.Cell(rowNum, 3).Value;
                                    LanSwitchDevice.Temperature_level = (int)worksheet.Cell(rowNum, 4).Value;
                                    LanSwitchDevice.FAN_status = (string)worksheet.Cell(rowNum, 5).Value == "Ok" ? "true" : "false";
                                    LanSwitchDevice.Bandwidth = (int)worksheet.Cell(rowNum, 6).Value;
                                    LanSwitchDevice.Power_source = (string)worksheet.Cell(rowNum, 7).Value == "Ok" ? "true" : "false";

                                    var NetworkRecordsDevice = new NetworkCreationDevice();
                                    NetworkRecordsDevice.Device = LanSwitchDevice.Device;
                                    NetworkRecordsDevice.CPU_processing = LanSwitchDevice.CPU_processing;
                                    NetworkRecordsDevice.Temperature_level = LanSwitchDevice.Temperature_level;
                                    NetworkRecordsDevice.FAN_status = LanSwitchDevice.FAN_status;
                                    NetworkRecordsDevice.Bandwidth = LanSwitchDevice.Bandwidth;
                                    NetworkRecordsDevice.Power_source = LanSwitchDevice.Power_source;
                                    await repositorioNetwork.CreateNetworkRecord(NetworkRecordsDevice);
                                }                              
                                if (worksheet.Name == "SWITCH ENDPOINTS")
                                {
                                    LanSwitchEndpoint.Device = (string)worksheet.Cell(rowNum, 2).Value;
                                    LanSwitchEndpoint.Port = (string)worksheet.Cell(rowNum, 3).Value;
                                    LanSwitchEndpoint.Description_port = (string)worksheet.Cell(rowNum, 4).Value;
                                    LanSwitchEndpoint.Category_issue = (string)worksheet.Cell(rowNum, 5).Value;
                                    LanSwitchEndpoint.Description_issue = (string)worksheet.Cell(rowNum, 6).Value;

                                    var NetworkRecordsEndpoint = new NetworkCreationEndpoint();
                                    NetworkRecordsEndpoint.Device = LanSwitchEndpoint.Device;
                                    NetworkRecordsEndpoint.Port = LanSwitchEndpoint.Port;
                                    NetworkRecordsEndpoint.Description_port = LanSwitchEndpoint.Description_port;
                                    NetworkRecordsEndpoint.Category_issue = LanSwitchEndpoint.Category_issue;
                                    NetworkRecordsEndpoint.Description_issue = LanSwitchEndpoint.Description_issue;
                                    await repositorioNetwork.CreateNetworkRecord(NetworkRecordsEndpoint);
                                }
                                if (worksheet.Name == "WLC")
                                {
                                  /*1*/  WLC.Acces_Point = (string)worksheet.Cell(rowNum, 2).Value;
                                 /*2*/   WLC.Number_connected_devices_2_4 = (int)worksheet.Cell(rowNum, 3).Value;
                                  /*3*/  WLC.Number_connected_devices_5 = (int)worksheet.Cell(rowNum, 4).Value;
                                  /*4*/  WLC.Canal_2_4 = (int)worksheet.Cell(rowNum, 5).Value;
                                  /*5*/  WLC.Canal_5 = (int)worksheet.Cell(rowNum, 6).Value;
                                  /*6*/  WLC.Frecuency_2_4 = (int)worksheet.Cell(rowNum, 7).Value;
                                   /*7*/ WLC.Frecuency_5 = (int)worksheet.Cell(rowNum, 8).Value;
                                   /*8*/ WLC.Number_SSID_propagated = (int)worksheet.Cell(rowNum, 9).Value;

                                    var NetworkRecordsWLC = new NetworkCreationWLC();
                                    NetworkRecordsWLC.Acces_Point = WLC.Acces_Point;
                                    NetworkRecordsWLC.Number_connected_devices_2_4 = WLC.Number_connected_devices_2_4;
                                    NetworkRecordsWLC.Number_connected_devices_5 = WLC.Number_connected_devices_5;
                                    NetworkRecordsWLC.Canal_2_4 = WLC.Canal_2_4;
                                    NetworkRecordsWLC.Canal_5 = WLC.Canal_5;
                                    NetworkRecordsWLC.Frecuency_2_4 = WLC.Frecuency_2_4;
                                    NetworkRecordsWLC.Frecuency_5 = WLC.Frecuency_5;
                                    NetworkRecordsWLC.Number_SSID_propagated = WLC.Number_SSID_propagated;
                                    await repositorioNetwork.CreateNetworkRecord(NetworkRecordsWLC);
                                }

                                //for (int colNum = 2; colNum <= 6; colNum++) //Columnas de la B a la I
                                //{
                                //    if (colNum == 2)
                                //    {
                                //        LanSwitchIssues.Device = (string)worksheet.Cell(rowNum, colNum).Value;
                                //        //LanSwitchDevice.Device = (string)worksheet.Cell(rowNum,colNum).Value;
                                //    }

                                //    if (colNum == 3)
                                //    {
                                //        LanSwitchIssues.Type_affectation = (string)worksheet.Cell(rowNum, colNum).Value;
                                //        //LanSwitchDevice.CPU_processing = (int)worksheet.Cell(rowNum, colNum).Value;
                                //    }

                                //    if (colNum == 4)
                                //    {
                                //        LanSwitchIssues.Description_affectation = (string)worksheet.Cell(rowNum, colNum).Value;
                                //        //LanSwitchDevice.Temperature_level = (int)worksheet.Cell(rowNum, colNum).Value;
                                //    }

                                //    if (colNum == 5)
                                //    {
                                //        LanSwitchIssues.Suggestion = (string)worksheet.Cell(rowNum, colNum).Value;
                                //        //LanSwitchDevice.FAN_status = (string)worksheet.Cell(rowNum, colNum).Value;
                                //    }

                                //    if (colNum == 6)
                                //    {
                                //        LanSwitchIssues.Criticality_level = (string)worksheet.Cell(rowNum, colNum).Value;
                                //        //LanSwitchDevice.Bandwidth = (int)worksheet.Cell(rowNum, colNum).Value;
                                //    }
                                //    //if (colNum == 7)
                                //    //{
                                //    //    LanSwitchDevice.Power_source = (string)worksheet.Cell(rowNum, colNum).Value;
                                //    //}
                                //}





                            }                        
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

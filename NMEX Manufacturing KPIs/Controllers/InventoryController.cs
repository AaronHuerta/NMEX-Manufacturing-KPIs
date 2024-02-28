using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Services;
using System.ComponentModel;

namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IRepositorioInventory repositorioInventory;
        private readonly IMapper mapper;

        public InventoryController(IRepositorioInventory repositorioInventory, IMapper mapper) {
            this.repositorioInventory = repositorioInventory;
            this.mapper = mapper;
        }

        //Inventory Accions -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var inventoryRecords = await repositorioInventory.GetInventoryRecords();
                return View(inventoryRecords);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateInventoryRecord()
        {
            try
            {
                var model = new InventoryCreationViewModel();
                model.DeviceTypes = await GetDevicesTypes();
                model.Versions = await GetVersions();
                model.Models = await GetModels();
                model.Locations = await GetLocations();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateInventoryRecord(InventoryCreationViewModel inventoryRecord)
        {
            try
            {
                await repositorioInventory.CreateInventoryRecord(inventoryRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditInventoryRecord(int id)
        {
            try
            {
                var inventoryRecord = await repositorioInventory.GetByIdInventoryRecord(id);
                var model = mapper.Map<InventoryCreationViewModel>(inventoryRecord);
                
                model.DeviceTypes = await GetDevicesTypes();
                model.Versions = await GetVersions();
                model.Models = await GetModels();
                model.Locations = await GetLocations();

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditInventoryRecord(InventoryCreationViewModel inventoryRecord)
        {
            try
            {
                await repositorioInventory.EditInventoryRecord(inventoryRecord);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                var inventoryRecord = await repositorioInventory.GetByIdInventoryRecord(id);

                return View(inventoryRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInventoryRecord(int Inventory_id)
        {
            try
            {
                await repositorioInventory.DeleteInventoryRecord(Inventory_id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetDevicesTypes()
        {
            var devicesTypes = await repositorioInventory.GetDevicesTypes();
            return devicesTypes.Select(x => new SelectListItem(x.D_type_description, x.D_type_id.ToString()));
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetVersions()
        {
            var versions = await repositorioInventory.GetVersions();
            return versions.Select(x => new SelectListItem(x.Version_description, x.Version_id.ToString()));
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetModels()
        {
            var models = await repositorioInventory.GetModels();
            return models.Select(x => new SelectListItem(x.Model_description, x.Model_id.ToString()));
        }

        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetLocations()
        {
            var locations = await repositorioInventory.GetLocations();
            return locations.Select(x => new SelectListItem(x.Location_description, x.Location_id.ToString()));
        }

        //Location Accions -----------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Location()
        {
            try
            {
                var locations = await repositorioInventory.GetLocations();
                return View(locations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateLocation()
        {
            try
            {
                var model = new LocationCreationViewModel();
                model.Plants = await GetPlants();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationCreationViewModel location)
        {
            try
            {
                await repositorioInventory.CreateLocation(location);
                return RedirectToAction(nameof(Location));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditLocation(int id)
        {
            try
            {
                var location = await repositorioInventory.GetByIdLocation(id);
                var model = mapper.Map<LocationCreationViewModel>(location);

                model.Plants = await GetPlants();

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditLocation(Location location)
        {
            try
            {
                await repositorioInventory.EditLocation(location);
                return RedirectToAction(nameof(Location));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }


        [HttpGet]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                var location = await repositorioInventory.GetByIdLocation(id);

                return View(location);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordLocation(int Location_id)
        {
            try
            {
                await repositorioInventory.DeleteLocation(Location_id);

                return RedirectToAction(nameof(Location));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }


        // Model Accions --------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Model()
        {
            try
            {
                var models = await repositorioInventory.GetModels();
                return View(models);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public IActionResult CreateModel()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Location));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Model model)
        {
            try
            {
                await repositorioInventory.CreateModel(model);
                return RedirectToAction(nameof(Model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditModel(int id)
        {
            try
            {
                var model = await repositorioInventory.GetByIdModel(id);

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditModel(Model model)
        {
            try
            {
                await repositorioInventory.EditModel(model);
                return RedirectToAction(nameof(Model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteModel(int id)
        {
            try
            {
                var model = await repositorioInventory.GetByIdModel(id);

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordModel(int Model_id)
        {
            try
            {
                await repositorioInventory.DeleteModel(Model_id);

                return RedirectToAction(nameof(Model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Model));
            }
        }

        //Version Accions --------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> Version()
        {
            try
            {
                var versions = await repositorioInventory.GetVersions();
                return View(versions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult CreateVersion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVersion(Models.Module_Inventory.Version version)
        {
            try
            {
                await repositorioInventory.CreateVersion(version);
                return RedirectToAction(nameof(Version));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditVersion(int id)
        {
            try
            {
                var version = await repositorioInventory.GetByIdVersion(id);

                return View(version);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditVersion(Models.Module_Inventory.Version version)
        {
            try
            {
                await repositorioInventory.EditVersion(version);
                return RedirectToAction(nameof(Version));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVersion(int id)
        {
            try
            {
                var version = await repositorioInventory.GetByIdVersion(id);

                return View(version);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordVersion(int Version_id)
        {
            try
            {
                await repositorioInventory.DeleteVersion(Version_id);

                return RedirectToAction(nameof(Version));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Version));
            }
        }

        //DeviceType Accions --------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> DeviceType()
        {
            try
            {
                var devicesTypes = await repositorioInventory.GetDevicesTypes();
                return View(devicesTypes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult CreateDeviceType()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeviceType(DeviceType deviceType)
        {
            try
            {
                await repositorioInventory.CreateDeviceType(deviceType);
                return RedirectToAction(nameof(DeviceType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditDeviceType(int id)
        {
            try
            {
                var deviceType = await repositorioInventory.GetByIdDeviceType(id);

                return View(deviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditDeviceType(DeviceType deviceType)
        {
            try
            {
                await repositorioInventory.EditDeviceType(deviceType);
                return RedirectToAction(nameof(DeviceType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDeviceType(int id)
        {
            try
            {
                var deviceType = await repositorioInventory.GetByIdDeviceType(id);

                return View(deviceType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteRecordDeviceType(int D_type_id)
        {
            try
            {
                await repositorioInventory.DeleteDeviceType(D_type_id);

                return RedirectToAction(nameof(DeviceType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(DeviceType));
            }
        }


        //Plant Accions --------------------------------------------------------------
        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetPlants()
        {
            var tiposAreas = await repositorioInventory.GetPlants();
            return tiposAreas.Select(x => new SelectListItem(x.Plant_description, x.Plant_id.ToString()));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Services;

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

            return View();
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

        //Plant Accions --------------------------------------------------------------
        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetPlants()
        {
            var tiposAreas = await repositorioInventory.GetPlants();
            return tiposAreas.Select(x => new SelectListItem(x.Plant_description, x.Plant_id.ToString()));
        }
    }
}

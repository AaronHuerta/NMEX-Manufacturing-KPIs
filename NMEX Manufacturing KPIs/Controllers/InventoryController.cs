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
                return RedirectToAction(nameof(Index));
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



        //Plant Accions --------------------------------------------------------------
        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetPlants()
        {
            var tiposAreas = await repositorioInventory.GetPlants();
            return tiposAreas.Select(x => new SelectListItem(x.Plant_description, x.Plant_id.ToString()));
        }
    }
}

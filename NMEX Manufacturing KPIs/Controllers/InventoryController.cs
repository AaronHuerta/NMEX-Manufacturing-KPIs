using Microsoft.AspNetCore.Mvc;
using NMEX_Manufacturing_KPIs.Services;

namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IRepositorioInventory repositorioInventory;

        public InventoryController(IRepositorioInventory repositorioInventory) {
            this.repositorioInventory = repositorioInventory;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var conjuntoPersonas = await repositorioInventory.Obtener();
            return View(conjuntoPersonas);
        }

        [HttpGet]
        public IActionResult CrearPersona()
        {

            return View();
        }
    }
}

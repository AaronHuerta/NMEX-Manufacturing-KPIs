using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models;
using NMEX_Manufacturing_KPIs.Models.Module_Security;
using NMEX_Manufacturing_KPIs.Services;


namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IRepositorioSecurity repositorioSecurity;
        private readonly IMapper mapper;


        public SecurityController(IRepositorioSecurity repositorioSecurity, IMapper mapper)
        {
            this.repositorioSecurity = repositorioSecurity;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var conjuntoPersonas = await repositorioSecurity.Obtener();
            return View(conjuntoPersonas);
        }

        [HttpGet]
        public async Task<IActionResult> SubCategory(int id)
        {
            try
            {
                var subCategories = await repositorioSecurity.GetByIdCategory(id);
                return View(subCategories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCalification(SubCategory subCategory)
        {
            try
            {
                // Realiza la lógica de actualización en la base de datos
                await repositorioSecurity.UpdateCalification(subCategory.SubCategory_id, subCategory.Result, subCategory.Comment);

                // Redirige a la acción original o a donde desees después de la actualización
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Puedes manejar el error y redirigir a una página de error o a la acción deseada
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            try
            {
                var model = new CategoryCreationViewModel();
                model.Function_names = await GetFunctions();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult CreateSubCategory(int id)
        {
            try
            {
                var model = new SubCategoryCreationViewModel
                {
                    Category_id = id
                    // Otras propiedades de inicialización si es necesario
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategoryCreationViewModel Category)
        {
            try
            {
                await repositorioSecurity.CreateSubCategory(Category);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreationViewModel category)
        {
            try
            {
                await repositorioSecurity.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var categories = await repositorioSecurity.GetDeleteByIdCategory(id);

                return View(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordCategory([Bind(Prefix = "Category_id")] int categoryId)
        {
            try
            {
                await repositorioSecurity.DeleteCategory(categoryId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                var Subcategories = await repositorioSecurity.GetDeleteByIdSubCategory(id);

                return View(Subcategories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRecordSubCategory(int SubCategory_id)
        {
            try
            {
                await repositorioSecurity.DeleteSubCategory(SubCategory_id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetFunctions()
        {
            var tiposAreas = await repositorioSecurity.GetFunctions();
            return tiposAreas.Select(x => new SelectListItem(x.Function_description, x.Function_id.ToString()));
        }

    }
}

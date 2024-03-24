using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models;
using NMEX_Manufacturing_KPIs.Models.Module_Security;
using NMEX_Manufacturing_KPIs.Services;
using System;
using System.IO;
using System.Collections;


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
            var IndexHome = await repositorioSecurity.Obtener();
            return View(IndexHome);
        }



        #region My UPDATE FILES CLASS and SUBCATEGORY

            [HttpGet]
            public IActionResult CreateDirectory()
            {
                try
                {
                    return View();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }


            [HttpGet]
            public async Task<IActionResult> SubCategory(int id)
            {
                try
                {
                    var subCategories = await repositorioSecurity.GetByIdCategory(id);

                    var processor = new RecursiveFileProcessor();
                    var paths = new List<string> { "C:\\Users\\Alonso.juan\\Desktop\\MainKPI_KPI\\NMEX Manufacturing KPIs\\Security\\" };
                    processor.ProcessPaths(paths);
                    var files = Directory.GetFiles(paths[0]);

                    foreach (var subCategory in subCategories)
                    {
                        subCategory.Files = files;
                    }

                    return View(subCategories);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction(nameof(Index));
                }
            }


            [HttpPost]
		    public async Task<IActionResult> UpdateCalification([FromForm] List<IFormFile> files, [FromForm] List<SubCategory> subCategories)
		    {
			    foreach (var subCategory in subCategories)
			    {
				    var plant = "Planta_" + subCategory.Plant_description.ToString();
				    var function = "Function_" + subCategory.Function_name.ToString();
				    var categoria = "Category_" + subCategory.Category_name.ToString();
				    var subCategoria = "SubCategoria_" + subCategory.SubCategory_id.ToString();
				    // Crear el directorio si no existe
				    var directoryPath = Path.Combine("C:\\Users\\Alonso.juan\\Desktop\\MainKPI_KPI\\NMEX Manufacturing KPIs\\Security\\" + plant + "\\" + function + "\\" + categoria + "\\" + subCategoria);
				    Directory.CreateDirectory(directoryPath);
				    // Guardar los archivos en el directorio
				    foreach (var file in files)
				    {
					    var filePath = Path.Combine("C:\\Users\\Alonso.juan\\Desktop\\MainKPI_KPI\\NMEX Manufacturing KPIs\\Security\\" + plant + "\\" + function + "\\" + categoria + "\\" + subCategoria, file.FileName);
					    using (var stream = System.IO.File.Create(filePath))
					    {
						    await file.CopyToAsync(stream);
					    }
				    }
				    // Realizar la lógica de actualización en la base de datos
				    await repositorioSecurity.UpdateCalification(subCategory.SubCategory_id, subCategory.Result, subCategory.Comment);
			    }
			    // Redirigir a la acción original o a donde desees después de la actualización
			    return RedirectToAction(nameof(Index));
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


        #endregion

        #region Filter by Plant

        #endregion

        #region CreateCategory
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
        private async Task<IEnumerable<SelectListItem>> GetFunctions()
        {
            var tiposAreas = await repositorioSecurity.GetFunctions();
            return tiposAreas.Select(x => new SelectListItem(x.Function_description, x.Function_id.ToString()));
        }

        #endregion

        #region DeleteCategory

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
        #endregion
    }
}

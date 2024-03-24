using AutoMapper;
using DapperLoginCRUD.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;
using NMEX_Manufacturing_KPIs.Models.Module_User;
using NMEX_Manufacturing_KPIs.Services;

namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<Users> userManager;
        private readonly IRepositorioUsers repositorioUsers;
        private readonly IMapper mapper;
        private readonly SignInManager<Users> signInManager;
        private readonly IServicioUsuario servicioUsuario;

        public UsersController(UserManager<Users> userManager, IRepositorioUsers repositorioUsers, IMapper mapper, SignInManager<Users> signInManager, IServicioUsuario servicioUsuario)
        {
            this.userManager = userManager;
            this.repositorioUsers = repositorioUsers;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.servicioUsuario = servicioUsuario;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var users = await repositorioUsers.GetUsersRecords();
                return View(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            try
            {
                var model = new RegisterViewModel();
                model.Plants = await GetPlants();
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            try
            {
                var user = new Users()
                {
                    FirstName = register.FirstName,
                    LastNamePaternal = register.LastNamePaternal,
                    LastNameMaternal = register.LastNameMaternal,
                    Email = register.Email,
                    Privilege = register.Privilege,
                    Plant_id = register.Plant_id
                };

                var resultado = await userManager.CreateAsync(user, password: register.Password);

                if (resultado.Succeeded)
                {
                    //await signInManager.SignInAsync(usuario, isPersistent: true); // Configuracion para que si el usuario ciera el navagador el usuario seguira autenticado en la aplicacion
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    var model = mapper.Map<RegisterViewModel>(register);

                    model.Plants = await GetPlants();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Register));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {


            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Inventory");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(model);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Inventory");
        }
        //Plant Accions --------------------------------------------------------------
        [HttpGet]
        private async Task<IEnumerable<SelectListItem>> GetPlants()
        {
            var tiposAreas = await repositorioUsers.GetPlants();
            return tiposAreas.Select(x => new SelectListItem(x.Plant_description, x.Plant_id.ToString()));
        }
    }
}

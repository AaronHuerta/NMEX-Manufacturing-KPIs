using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NMEX_Manufacturing_KPIs.Models;
//using NMEX_Manufacturing_KPIs.Models.Module_Network;
using NMEX_Manufacturing_KPIs.Services;

namespace NMEX_Manufacturing_KPIs.Controllers
{
    public class AgingSoftwareController : Controller
    {

        private readonly IRepositorioAgingSoftware repositorioAgingSoftware;
        private readonly IMapper mapper;


        public AgingSoftwareController(IRepositorioAgingSoftware repositorioAgingSoftware, IMapper mapper)
        {
            this.repositorioAgingSoftware = repositorioAgingSoftware;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

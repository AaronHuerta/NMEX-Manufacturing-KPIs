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
    public class AgingHardwareController : Controller
    {

        private readonly IRepositorioAgingHardware repositorioAgingHardware;
        private readonly IMapper mapper;


        public AgingHardwareController(IRepositorioAgingHardware repositorioAgingHardware, IMapper mapper)
        {
            this.repositorioAgingHardware = repositorioAgingHardware;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}

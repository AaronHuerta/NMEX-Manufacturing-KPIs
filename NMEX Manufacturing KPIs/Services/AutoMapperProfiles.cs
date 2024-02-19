using AutoMapper;
using NMEX_Manufacturing_KPIs.Models.Module_Inventory;


namespace NMEX_Manufacturing_KPIs.Services
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Location, LocationCreationViewModel>();



        }
    }
}

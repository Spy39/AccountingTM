using AutoMapper;
using AccountingTM.ViewModels;
using AccountingTM.Dto;
using Accounting.Models;
using AccountingTM.Dto.TechnicalEquipment;
using AccountingTM.Domain.Models.Tables;
using AccountingTM.Models;

namespace AccountingTM.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TechnicalEquipment, GetAllTechnicalDto>();
            CreateMap<GetAllTechnicalDto, TechnicalEquipment>();
        }

    }
}

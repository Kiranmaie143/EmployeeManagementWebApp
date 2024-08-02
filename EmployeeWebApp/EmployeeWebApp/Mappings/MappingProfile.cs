using AutoMapper;
using EmployeeWebApp.DTO;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}

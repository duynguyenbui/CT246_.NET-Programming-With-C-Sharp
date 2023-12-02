using AutoMapper;
using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Web.RequestHelper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, UpdateEmployeeDto>();
    }
}
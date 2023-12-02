using AutoMapper;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Entities;

namespace EmployeeManagement.API.RequestHelper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
        CreateMap<CreateDepartmentDto, Department>();
    }
}
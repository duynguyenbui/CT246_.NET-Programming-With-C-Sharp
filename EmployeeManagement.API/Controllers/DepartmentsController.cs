using System.Data;
using AutoMapper;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/departments")]
[Authorize]
public class DepartmentsController(EmpManagementContext context, IDepartmentRepository departmentRepository,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
    {
        return await context.Deparments.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Department>> GetDepartments(int departmentId)
    {
        return await context.Deparments.FindAsync(departmentId) ?? new Department();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Department>> CreateDepartment(CreateDepartmentDto createDepartmentDto)
    {
        var department = mapper.Map<Department>(createDepartmentDto);
        try
        {
            var departmentSaved = await departmentRepository.CreateDepartment(department);
            return Ok(departmentSaved);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(new ProblemDetails()
            {
                Title = "Error creating department",
                Detail = e.ToString()
            });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteDepartment(int id)
    {
        try
        {
            var result = await departmentRepository.DeleteDepartment(id);
            if (result > 0)
            {
                return Ok();
            }
            throw new DataException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

}
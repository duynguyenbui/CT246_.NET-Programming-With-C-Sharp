using AutoMapper;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Repositories.Contracts;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController
    (IEmployeeRepository employeeRepository, IMapper mapper, ImagesService imagesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Employee>> GetEmployees()
    {
        var employees = await employeeRepository.GetEmployees();
        if (employees.Any())
        {
            return Ok(employees);
        }

        return NotFound();
    }

    [HttpGet("{employeeId:int}", Name = "GetEmployee")]
    [Authorize]
    public async Task<ActionResult<Employee>> GetEmployee(int employeeId)
    {
        var employee = await employeeRepository.GetEmployee(employeeId);
        if (employee is not null)
        {
            return Ok(employee);
        }

        return NotFound("Not found employee");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Employee>> CreateEmployee([FromForm] CreateEmployeeDto createEmployeeDto)
    {
        if (createEmployeeDto == null)
        {
            return BadRequest(new ProblemDetails()
            {
                Title = "Invalid Create Employee Model",
            });
        }

        var employee = mapper.Map<Employee>(createEmployeeDto);
        
        var imageResult = await imagesService.AddImageAsync(createEmployeeDto.File);
        if (imageResult.Error != null)
        {
            return BadRequest(new ProblemDetails()
                { Title = "Error uploading image", Detail = imageResult.Error.ToString() });
        }

        employee.PhotoPath = imageResult.SecureUrl.ToString();
        try
        {
            var employeeSaved = await employeeRepository.AddEmployee(employee);
            return Ok(employeeSaved);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            return BadRequest(new ProblemDetails() { Title = "Internal Server Error" });
        }
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Employee>> UpdateEmployee(UpdateEmployeeDto? updateEmployeeDto)
    {
        if (updateEmployeeDto is null) return BadRequest();
        var employee = mapper.Map<Employee>(updateEmployeeDto);
        var updateEmployee = await employeeRepository.UpdateEmployee(employee);

        if (updateEmployee is not null)
        {
            return Ok(updateEmployee);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("{employeeId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteEmployee(int employeeId)
    {
        var deleteEmployee = await employeeRepository.DeleteEmployee(employeeId);
        return Ok(deleteEmployee);
    }

    private async Task<IFormFile> Base64ToImage(string base64String, string fileName)
    {
        byte[] bytes = Convert.FromBase64String(base64String);
        MemoryStream stream = new MemoryStream(bytes);
        IFormFile file = new FormFile(stream, 0, bytes.Length, fileName, fileName);
        return file;
    }
}
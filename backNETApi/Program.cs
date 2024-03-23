using backNETApi.Models;
using Microsoft.EntityFrameworkCore;
using backNETApi.Services.Contract;
using backNETApi.Services.Implementation;

using AutoMapper;
using backNETApi.DTOs;
using backNETApi.Utilidades;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// add the context for the builder of the db context
// cadenaSQL is defined on appsettings.json

builder.Services.AddDbContext<DbemployeeContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));


});

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

 builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddCors(options =>
{
    options.AddPolicy("newPolicy", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region Peticiones API REST
app.MapGet("/department/list", async (

    IDepartmentService _departmentService,
    IMapper mapper
    ) =>
{
    List<Department> DepartmentList = await _departmentService.getDepartments();
    List<DepartmentDTO> departmentListDTO= mapper.Map<List<DepartmentDTO>>(DepartmentList);
    return departmentListDTO.Count() > 0 ? Results.Ok(departmentListDTO) : Results.NotFound();
});

app.MapGet("/employee/list", async (

    IEmployeeService _employeeService,
    IMapper mapper
    ) =>
{
    List<Employee> EmployeeList = await _employeeService.GetAll();
    List<employeeDTO> employeeListDTO = mapper.Map<List<employeeDTO>>(EmployeeList);
    return employeeListDTO.Count() > 0 ? Results.Ok(employeeListDTO) : Results.NotFound();
});


app.MapPost("/employee/save", async(
    employeeDTO modelDTO,
    IEmployeeService employeeService,
    IMapper mapper
    ) => {
        var _employee = mapper.Map<Employee>(modelDTO);
        var _CreatedEmployee=await employeeService.Add(_employee);
        if (_CreatedEmployee.IdEmployee != 0)
        {
            return Results.Ok(mapper.Map<employeeDTO>(_CreatedEmployee));
        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }


    });

app.MapPut("/employee/update/{idEmployee}", async (
    int idEmployee,
    employeeDTO model,
    IEmployeeService employeeService,
    IMapper mapper
    ) =>
{ 
    var found =await employeeService.GetById(idEmployee);
    if(found is null) { return Results.NotFound(); }
    var employee=mapper.Map<Employee>(model);

    found.Name = employee.Name;
    found.Surname = employee.Surname;
    found.Salary = employee.Salary;
    found.ContractDate = employee.ContractDate;
    found.IdDepartment = employee.IdDepartment;

    var answer=await employeeService.Update(found);

    if (answer)
    {
        return Results.Ok(mapper.Map<employeeDTO>(found));
    }
    else
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

}) ;

app.MapDelete("/employee/delete/{idEmployee}",async(
    int idEmployee,
    IEmployeeService employeeService
    ) => {

        var found = await employeeService.GetById(idEmployee);
        if (found is null) { return Results.NotFound(); }
        var answer = await employeeService.Delete(found);
        if (answer)
        {
            return Results.Ok();
        }
        else
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

    });
#endregion

app.UseCors("newPolicy");
app.UseAuthorization();

app.MapControllers();


app.Run();

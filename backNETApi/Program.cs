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





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region Peticiones API REST
app.MapGet("/department/list2", async (

    IDepartmentService _departmentService,
    IMapper mapper
    ) =>
{
    List<Department> DepartmentList = await _departmentService.getDepartments();
    List<DepartmentDTO> departmentListDTO= mapper.Map<List<DepartmentDTO>>(DepartmentList);
    return departmentListDTO.Count() > 0 ? Results.Ok(departmentListDTO) : Results.NotFound();
});

#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();

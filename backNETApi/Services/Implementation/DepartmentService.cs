using Microsoft.EntityFrameworkCore;
using backNETApi.Models;
using backNETApi.Services.Contract;

namespace backNETApi.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private DbemployeeContext _employeeContext;

        public DepartmentService(DbemployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task<List<Department>> getDepartments()
        {
            try
            {
                List<Department> list= new List<Department>();
                list = await _employeeContext.Departments.ToListAsync();
                return list;
            }catch (Exception ex) { throw ex; }
        }
    }
}

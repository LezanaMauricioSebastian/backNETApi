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

        public async Task<bool> Delete(Department department)
        {
            try
            {
                _employeeContext.Departments.Remove(department);
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<bool> Update(Department department)
        {
            try
            {
                _employeeContext.Departments.Update(department);
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Department> GetById(int id)
        {
            try
            {
                Department? department = new Department();
                department = await _employeeContext.Departments.Where(e => e.IdDepartment == id).FirstOrDefaultAsync();
                return department;
            }
            catch (Exception ex) { throw ex; }
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

        public async Task<Department> Add(Department department)
        {
            try
            {
                _employeeContext.Departments.Add(department);
                await _employeeContext.SaveChangesAsync();
                return department;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

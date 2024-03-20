using Microsoft.EntityFrameworkCore;
using backNETApi.Models;
using backNETApi.Services.Contract;

namespace backNETApi.Services.Implementation
{
    
    public class EmployeeService : IEmployeeService
    {
        private DbemployeeContext _employeeContext;

        public EmployeeService(DbemployeeContext dbemployee) {
            _employeeContext= dbemployee;
        }

        public async Task<Employee> Add(Employee employee)
        {
            try
            {
                _employeeContext.Employees.Add(employee);
                await _employeeContext.SaveChangesAsync();
                return employee;
            }catch(Exception ex) { throw ex; }
        }

        public async Task<bool> Delete(Employee employee)
        {
            try
            {
                _employeeContext.Employees.Remove(employee);
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<Employee>> GetAll()
        {
            try
            {
                List<Employee> list = new List<Employee>();
                list = await _employeeContext.Employees.Include(dpt => dpt.IdDepartmentNavigation).ToListAsync();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Employee> GetById(int id)
        {
            try
            {
                Employee? employee = new Employee();
                employee = await _employeeContext.Employees.Include(dpt => dpt.IdDepartmentNavigation).Where(e => e.IdEmployee == id).FirstOrDefaultAsync();
                return employee;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<bool> Update(Employee employee)
        {
            try
            {
                _employeeContext.Employees.Update(employee);
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

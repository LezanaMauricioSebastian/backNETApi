using backNETApi.Models;

namespace backNETApi.Services.Contract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<Employee> Add(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(Employee employee);
    
    }
}

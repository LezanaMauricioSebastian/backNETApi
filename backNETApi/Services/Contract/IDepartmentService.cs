using backNETApi.Models;

namespace backNETApi.Services.Contract
{
    public interface IDepartmentService
    {
        Task<List<Department>> getDepartments();
        Task<Department> Add(Department department);
        Task<Department> GetById(int id);
        Task<bool> Update(Department department);
        Task<bool> Delete(Department department);
    }
}

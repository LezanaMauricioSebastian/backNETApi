using backNETApi.Models;

namespace backNETApi.Services.Contract
{
    public interface IDepartmentService
    {
        Task<List<Department>> getDepartments();
    }
}

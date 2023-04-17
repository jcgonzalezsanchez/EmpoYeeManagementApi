using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;

namespace EmployeeManagement.Contracts.Interfaces.Services
{
    public interface IJobPositionService
    {
        Task<PagedResponse<IEnumerable<JobPosition>>> ListAsync(PaginationParameter filter);
        Task<JobPositionResponse> FindByIdAsync(int id);
        Task<JobPositionResponse> SaveAsync(JobPosition jobPosition);
        Task<JobPositionResponse> Update(JobPosition jobPosition);
        Task<JobPositionResponse> Delete(int id);
    }
}

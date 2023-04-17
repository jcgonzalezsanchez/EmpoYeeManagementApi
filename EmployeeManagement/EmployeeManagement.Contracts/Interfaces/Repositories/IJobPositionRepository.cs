using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;

namespace EmployeeManagement.Contracts.Interfaces.Repositories
{
    public interface IJobPositionRepository
    {
        Task<PagedResponse<IEnumerable<JobPosition>>> ListAsync(PaginationParameter filter);
        Task<JobPosition?> FindByIdAsync(int id);
        Task SaveAsync(JobPosition jobPosition);
        void Update(JobPosition jobPosition);
        void Remove(JobPosition jobPosition);
    }
}

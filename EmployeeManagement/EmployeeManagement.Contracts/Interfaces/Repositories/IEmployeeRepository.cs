using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;

namespace EmployeeManagement.Contracts.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<PagedResponse<IEnumerable<Employee>>> ListAsync(PaginationParameter filter);
        Task<Employee?> FindByIdAsync(int id);
        Task SaveAsync(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
    }
}

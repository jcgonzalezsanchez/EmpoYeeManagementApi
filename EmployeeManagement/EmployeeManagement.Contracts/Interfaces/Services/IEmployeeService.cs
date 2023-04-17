using EmployeeManagement.Contracts.Dto;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;

namespace EmployeeManagement.Contracts.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<PagedResponse<IEnumerable<Employee>>> ListAsync(PaginationParameter filter);
        Task<EmployeeResponse> FindByIdAsync(int id);
        Task<EmployeeResponse> SaveAsync(Employee employee);
        Task<EmployeeResponse> Update(Employee employee);
        Task<EmployeeResponse> Delete(int id);
        Task<EmployeeResponse> SaveImageAsync(ImageDto image);
        Task<EmployeeResponse> UpdateImageAsync(ImageDto image);
        Task<EmployeeResponse> DeleteImageAsync(int id);
    }
}

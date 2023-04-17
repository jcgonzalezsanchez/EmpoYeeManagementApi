using EmployeeManagement.Contracts.Helpers;
using EmployeeManagement.Contracts.Interfaces.Repositories;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;
using EmployeeManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<Employee>>> ListAsync(PaginationParameter filter)
        {
            return await _context.Employees
                                 .AsNoTracking()
                                 .PaginateAsync(filter);
        }

        public async Task<Employee?> FindByIdAsync(int id)
        {
            return await _context.Employees
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            _context.SaveChanges();
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void Remove(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}

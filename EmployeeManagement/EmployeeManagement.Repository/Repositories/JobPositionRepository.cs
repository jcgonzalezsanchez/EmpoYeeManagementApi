using EmployeeManagement.Contracts.Helpers;
using EmployeeManagement.Contracts.Interfaces.Repositories;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;
using EmployeeManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Repositories
{
    public class JobPositionRepository : BaseRepository, IJobPositionRepository
    {
        public JobPositionRepository(AppDbContext context) : base(context) { }

        public async Task<PagedResponse<IEnumerable<JobPosition>>> ListAsync(PaginationParameter filter)
        {
            return await _context.JobPositions
                                 .AsNoTracking()
                                 .PaginateAsync(filter);
        }

        public async Task<JobPosition?> FindByIdAsync(int id)
        {
            return await _context.JobPositions
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveAsync(JobPosition jobPosition)
        {
            await _context.JobPositions.AddAsync(jobPosition);
            _context.SaveChanges();
        }

        public void Update(JobPosition jobPosition)
        {
            _context.JobPositions.Update(jobPosition);
            _context.SaveChanges();
        }

        public void Remove(JobPosition jobPosition)
        {
            _context.JobPositions.Remove(jobPosition);
            _context.SaveChanges();
        }
    }
}

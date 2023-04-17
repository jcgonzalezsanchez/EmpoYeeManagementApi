using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Contracts.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationParameter validFilter, int totalRecords)
        {
            var respose = new PagedResponse<IEnumerable<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static async Task<PagedResponse<IEnumerable<TModel>>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            PaginationParameter filter)
            where TModel : class
        {
            var totalItemsCountTask = query.Count();

            var startRow = filter.PageNumber * filter.PageSize;

            if (totalItemsCountTask <= startRow) { startRow = 0; }

            var data = await query
                   .Skip(startRow)
                   .Take(filter.PageSize)
                   .ToListAsync();

            return CreatePagedReponse(data, filter, totalItemsCountTask);
        }
    }
}

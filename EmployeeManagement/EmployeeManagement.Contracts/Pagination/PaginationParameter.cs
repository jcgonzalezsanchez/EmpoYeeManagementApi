namespace EmployeeManagement.Contracts.Pagination
{
    public class PaginationParameter
    {
        /// <summary>
        /// Gets or sets the page number of the pagination filter.
        /// </summary>
        /// <value>The page number's pagination filter.</value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the page size of the pagination filter.
        /// </summary>
        /// <value>The page size's pagination filter.</value>
        public int PageSize { get; set; }

        public PaginationParameter()
        {
            this.PageNumber = 0;
            this.PageSize = 10;
        }

        public PaginationParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 0 ? 0 : pageNumber;
            this.PageSize = pageSize < 1 ? 10 : pageSize > 100 ? 100 : pageSize;
        }
    }
}

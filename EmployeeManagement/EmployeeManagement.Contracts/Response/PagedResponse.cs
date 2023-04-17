namespace EmployeeManagement.Contracts.Response
{
    public class PagedResponse<T> : BaseResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="data">Saved data.</param>
        /// <returns>Response.</returns>
        public PagedResponse(T data, int pageNumber, int pageSize) : base(data)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

    }
}

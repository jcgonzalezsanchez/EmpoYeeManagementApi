namespace EmployeeManagement.Contracts.Pagination
{
    public class QueryParameter : PaginationParameter
    {
        public virtual string OrderBy { get; set; }
        public virtual string Search { get; set; }

        public QueryParameter()
        {
            this.PageNumber = 0;
            this.PageSize = 10;
            this.OrderBy = "Id";
            this.Search = "";
        }

        public QueryParameter(int pageNumber, int pageSize, string oderby, string search)
        {
            this.PageNumber = pageNumber < 0 ? 0 : pageNumber;
            this.PageSize = pageSize < 1 ? 10 : pageSize > 100 ? 30 : pageSize;
            this.OrderBy = string.IsNullOrEmpty(oderby) ? "Id" : oderby;
            this.Search = string.IsNullOrEmpty(search) ? "" : search;
        }
    }
}

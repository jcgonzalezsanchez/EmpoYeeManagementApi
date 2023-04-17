using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Contracts.Response
{
    public class EmployeeResponse : BaseResponse<Employee>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="employee">Saved Employee.</param>
        /// <returns>Response.</returns>
        public EmployeeResponse(Employee employee) : base(employee)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public EmployeeResponse(string message) : base(message)
        { }
    }
}

using EmployeeManagement.Contracts.Models;

namespace EmployeeManagement.Contracts.Response
{
    public class JobPositionResponse : BaseResponse<JobPosition>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="jobPosition">Saved Job position.</param>
        /// <returns>Response.</returns>
        public JobPositionResponse(JobPosition jobPosition) : base(jobPosition)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public JobPositionResponse(string message) : base(message)
        { }
    }
}

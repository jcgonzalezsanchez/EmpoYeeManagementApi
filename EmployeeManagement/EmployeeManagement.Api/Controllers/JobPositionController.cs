using EmployeeManagement.Contracts.Interfaces.Services;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Resources;
using EmployeeManagement.Contracts.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class JobPositionController : Controller
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        /// <summary>
        /// Get all Job Position.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Job Positions</returns>
        /// <response code="200">The Job Positions were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<JobPosition>>), 200)]
        public async Task<PagedResponse<IEnumerable<JobPosition>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _jobPositionService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Job Position by id.
        /// </summary>
        /// <returns>The Job Position.</returns>
        /// <response code="200">The Job Position was successfully retrieved.</response> 
        /// <response code="404">The Job Position does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(JobPosition), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            var result = await _jobPositionService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Job Position.
        /// </summary>
        /// <param name="employee">The Job Position object.</param>
        /// <returns>The created Job Position.</returns>
        /// <response code="201">The Job Position was successfully created.</response>
        /// <response code="400">The Job Position is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(JobPosition), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<ActionResult> PostAsync([FromBody] JobPosition jobPosition)
        {
            var result = await _jobPositionService.SaveAsync(jobPosition);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Job Position.
        /// </summary>
        /// <param name = "employee" > The Job Position object.</param>
        /// <returns>The updated Job Position.</returns>
        /// <response code = "200" > The Job Position was successfully updated.</response>
        /// <response code = "400" > The Job Position is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(JobPosition), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] JobPosition jobPosition)
        {
            var result = await _jobPositionService.Update(jobPosition);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Job Position.
        /// </summary>
        /// <param name="id">The Job Position id.</param>
        /// <returns>The deleted Job Position.</returns>
        /// <response code="200">The Job Position was successfully deleted.</response>
        /// <response code="400">The Job Position is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(JobPosition), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _jobPositionService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}

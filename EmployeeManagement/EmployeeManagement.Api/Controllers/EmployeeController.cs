using EmployeeManagement.Contracts.Dto;
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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all Employees.
        /// </summary>
        /// <param name="filter">The pagination filter</param>
        /// <returns>The list of Employees</returns>
        /// <response code="200">The Employees were successfully retrieved.</response> 
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<Employee>>), 200)]
        public async Task<PagedResponse<IEnumerable<Employee>>> ListAsync([FromQuery] PaginationParameter filter)
        {
            var validFilter = new PaginationParameter(filter.PageNumber, filter.PageSize);
            return await _employeeService.ListAsync(validFilter);
        }

        /// <summary>
        /// Get a Employee by id.
        /// </summary>
        /// <returns>The Employee.</returns>
        /// <response code="200">The Employee was successfully retrieved.</response> 
        /// <response code="404">The Employee does not exist.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResource), 404)]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            var result = await _employeeService.FindByIdAsync(id);

            if (!result.Success)
            {
                return new ObjectResult(new ErrorResource(result.Message)) { StatusCode = 404 };
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Employee.
        /// </summary>
        /// <param name="employee">The Employee object.</param>
        /// <returns>The created Employee.</returns>
        /// <response code="201">The Employee was successfully created.</response>
        /// <response code="400">The Employee is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Employee), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<ActionResult> PostAsync([FromBody] Employee employee)
        {
            var result = await _employeeService.SaveAsync(employee);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Employee.
        /// </summary>
        /// <param name = "employee" > The Employee object.</param>
        /// <returns>The updated Employee.</returns>
        /// <response code = "200" > The Employee was successfully updated.</response>
        /// <response code = "400" > The Employee is invalid.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            var result = await _employeeService.Update(employee);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Employee.
        /// </summary>
        /// <param name="id">The Employee id.</param>
        /// <returns>The deleted Employee.</returns>
        /// <response code="200">The Employee was successfully deleted.</response>
        /// <response code="400">The Employee is invalid.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Create a new Image.
        /// </summary>
        /// <param name="image">The Image object.</param>
        /// <returns>The created Image.</returns>
        /// <response code="201">The Image was successfully created.</response>
        /// <response code="400">The Image is invalid.</response>
        [HttpPost]
        [Route("/Image")]
        [ProducesResponseType(typeof(ImageDto), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostImageAsync([FromForm] ImageDto image)
        {
            var result = await _employeeService.SaveImageAsync(image);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return new ObjectResult(result.Resource) { StatusCode = 201 };
        }

        /// <summary>
        /// Update an existing Image.
        /// </summary>
        /// <param name = "image" > The Image object.</param>
        /// <returns>The updated Image.</returns>
        /// <response code = "200" > The Image was successfully updated.</response>
        /// <response code = "400" > The Image is invalid.</response>
        [HttpPut]
        [Route("/Image")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> UpdateImageAsync([FromForm] ImageDto image)
        {
            var result = await _employeeService.UpdateImageAsync(image);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        /// <summary>
        /// Delete a Image.
        /// </summary>
        /// <param name="id">The Image id.</param>
        /// <returns>The deleted Image.</returns>
        /// <response code="200">The Image was successfully deleted.</response>
        /// <response code="400">The Image is invalid.</response>
        [HttpDelete]
        [Route("/Image/{id}")]
        [ProducesResponseType(typeof(Employee), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteImageAsync(int id)
        {
            var result = await _employeeService.DeleteImageAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}

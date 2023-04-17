using EmployeeManagement.Contracts.Dto;
using EmployeeManagement.Contracts.Helpers;
using EmployeeManagement.Contracts.Interfaces.Repositories;
using EmployeeManagement.Contracts.Interfaces.Services;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IStorageRepository storageRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _storageRepository = storageRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<Employee>>> ListAsync(PaginationParameter filter)
        {
            var employees = await _employeeRepository.ListAsync(filter);

            if (employees.Resource.Any())
                _logger.LogInformation($"Employee Service - {employees.Resource.Count()} Employees listed successfully.");
            else
                _logger.LogError("Employee Service - No Employees to list.");

            return employees;
        }

        public async Task<EmployeeResponse> FindByIdAsync(int id)
        {
            try
            {
                var employee = await _employeeRepository.FindByIdAsync(id);

                if (employee == null)
                {
                    _logger.LogInformation($"Employee Service - Employee Id '{id}' not found.");
                    return new EmployeeResponse("Employee not found.");
                }

                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error retrieving the Employee: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when retrieving the Employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> SaveAsync(Employee employee)
        {
            try
            {
                await _employeeRepository.SaveAsync(employee);
                _logger.LogInformation($"Employee Service - Employee Id '{employee.Id}' saved successfully.");

                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error saving the Employee: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when saving the Employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> Update(Employee employee)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(employee.Id);

            if (existingEmployee == null)
            {
                _logger.LogInformation($"Employee Service - Employee Id '{0}' not found.", employee.Id);
                return new EmployeeResponse("Employee not found.");
            }

            try
            {
                _employeeRepository.Update(employee);
                _logger.LogInformation($"Employee Service - Employee Id '{0}' updated successfully.", employee.Id);
                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error updating the Employee: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when updating the Employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> Delete(int id)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
            {
                _logger.LogInformation($"Employee Service - Employee Id '{0}' not found.", id);
                return new EmployeeResponse("Employee not found.");
            }

            try
            {
                _employeeRepository.Remove(existingEmployee);
                _logger.LogInformation($"Employee Service - Employee Id '{0}' deleted successfully.", id);
                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error deleting the Employee: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when deleting the Employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> SaveImageAsync(ImageDto image)
        {
            var employee = await _employeeRepository.FindByIdAsync(image.EmployeeId);

            if (employee == null)
            {
                _logger.LogInformation($"Employee Service - Employee Id '{image.EmployeeId}' not found.");
                return new EmployeeResponse("Employee not found.");
            }

            var checkExtension = CheckExtensionHelper.CheckExtension(Path.GetExtension(image.Image.FileName));

            if (!checkExtension)
            {
                _logger.LogInformation($"Employee Service - Only .PNG and .JPG formats are allowed. Theformat uploaded is not valid");
                return new EmployeeResponse($"Employee Service - Only .PNG and .JPG formats are allowed. The {image.Image.FileName} format uploaded is not valid");
            }

            try
            {
                var stream = new MemoryStream();
                image.Image.CopyTo(stream);
                byte[] contentBytes = stream.ToArray();
                var urlImage = await _storageRepository.UploadFileAsync(contentBytes, image.Image.FileName);

                if (urlImage == null)
                {
                    _logger.LogInformation($"Enployee Service - An error occurred while loading the image.");
                    return new EmployeeResponse($"An error occurred while loading the image.");
                }

                employee.UrlImage = urlImage;
                employee.FileName = image.Image.FileName;
                _employeeRepository.Update(employee);
                _logger.LogInformation($"Employee Service - Image updated successfully.", employee.Id);
                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error saving the Image; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when saving the Image: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateImageAsync(ImageDto image)
        {
            var employee = await _employeeRepository.FindByIdAsync(image.EmployeeId);

            if (employee == null)
            {
                _logger.LogInformation($"Employee Service - Employee Id '{image.EmployeeId}' not found.");
                return new EmployeeResponse("Employee not found.");
            }

            var checkExtension = CheckExtensionHelper.CheckExtension(Path.GetExtension(image.Image.FileName));

            if (!checkExtension)
            {
                _logger.LogInformation($"Employee Service - Only .PNG and .JPG formats are allowed. Theformat uploaded is not valid");
                return new EmployeeResponse($"Employee Service - Only .PNG and .JPG formats are allowed. The {image.Image.FileName} format uploaded is not valid");
            }

            try
            {
                var stream = new MemoryStream();
                image.Image.CopyTo(stream);
                byte[] contentBytes = stream.ToArray();
                var urlImage = await _storageRepository.UpdateFileAsync(contentBytes, employee.FileName ?? string.Empty, image.Image.FileName);

                if (urlImage == null)
                {
                    _logger.LogInformation($"Employee Service - An error occurred while updating the image.");
                    return new EmployeeResponse($"An error occurred while updating the image.");
                }

                employee.UrlImage = urlImage;
                employee.FileName = image.Image.FileName;
                _employeeRepository.Update(employee);
                _logger.LogInformation($"Employee Service - Image updated successfully.", employee.Id);
                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error saving the Image; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when saving the Image: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> DeleteImageAsync(int id)
        {
            var employee = await _employeeRepository.FindByIdAsync(id);

            if (employee == null)
            {
                _logger.LogInformation($"Employee Service - Employee Id '{id}' not found.");
                return new EmployeeResponse("Employee not found.");
            }

            try
            {
                if (!string.IsNullOrEmpty(employee.FileName))
                {
                    await _storageRepository.DeleteFileAsync(employee.FileName);
                }

                employee.UrlImage = null;
                employee.FileName = null;
                _employeeRepository.Update(employee);
                _logger.LogInformation($"Employee Service - Image deleted successfully.", employee.Id);
                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee Service - Error deleting the Image; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new EmployeeResponse($"An error occurred when deleting the Image: {ex.Message}");
            }
        }
    }
}

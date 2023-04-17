using EmployeeManagement.Contracts.Interfaces.Repositories;
using EmployeeManagement.Contracts.Interfaces.Services;
using EmployeeManagement.Contracts.Models;
using EmployeeManagement.Contracts.Pagination;
using EmployeeManagement.Contracts.Response;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Service.Services
{
    public class JobPositionService : IJobPositionService
    {
        private readonly IJobPositionRepository _jobPositionRepository;
        private readonly ILogger<JobPositionService> _logger;

        public JobPositionService(IJobPositionRepository jobPositionRepository, ILogger<JobPositionService> logger)
        {
            _jobPositionRepository = jobPositionRepository;
            _logger = logger;

        }
        public async Task<PagedResponse<IEnumerable<JobPosition>>> ListAsync(PaginationParameter filter)
        {
            var jobPositions = await _jobPositionRepository.ListAsync(filter);

            if (jobPositions.Resource.Any())
                _logger.LogInformation($"Job Position Service - {jobPositions.Resource.Count()} Job Positions listed successfully.");
            else
                _logger.LogError("Job Position Service - No Job Positions to list.");

            return jobPositions;
        }

        public async Task<JobPositionResponse> FindByIdAsync(int id)
        {
            try
            {
                var jobPosition = await _jobPositionRepository.FindByIdAsync(id);

                if (jobPosition == null)
                {
                    _logger.LogInformation($"Job Position Service - Job Position Id '{id}' not found.");
                    return new JobPositionResponse("Job Position not found.");
                }

                return new JobPositionResponse(jobPosition);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Job Position Service - Error retrieving the Job Position: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new JobPositionResponse($"An error occurred when retrieving the Job Position: {ex.Message}");
            }
        }

        public async Task<JobPositionResponse> SaveAsync(JobPosition jobPosition)
        {
            try
            {
                await _jobPositionRepository.SaveAsync(jobPosition);
                _logger.LogInformation($"Job Position Service - Job Position Id '{jobPosition.Id}' saved successfully.");

                return new JobPositionResponse(jobPosition);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Job Position Service - Error saving the Job Position: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new JobPositionResponse($"An error occurred when saving the Job Position: {ex.Message}");
            }
        }

        public async Task<JobPositionResponse> Update(JobPosition jobPosition)
        {
            var existingJobPosition = await _jobPositionRepository.FindByIdAsync(jobPosition.Id);

            if (existingJobPosition == null)
            {
                _logger.LogInformation($"Job Position Service - Job Position Id '{0}' not found.", jobPosition.Id);
                return new JobPositionResponse("Job Position not found.");
            }

            try
            {
                _jobPositionRepository.Update(jobPosition);
                _logger.LogInformation($"Job Position Service - Job Position Id '{0}' updated successfully.", jobPosition.Id);
                return new JobPositionResponse(jobPosition);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Job Position Service - Error updating the Job Position: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new JobPositionResponse($"An error occurred when updating the Job Position: {ex.Message}");
            }
        }

        public async Task<JobPositionResponse> Delete(int id)
        {
            var existingJobPosition = await _jobPositionRepository.FindByIdAsync(id);

            if (existingJobPosition == null)
            {
                _logger.LogInformation($"Job Position Service - Job Position Id '{0}' not found.", id);
                return new JobPositionResponse("Job Position not found.");
            }

            try
            {
                _jobPositionRepository.Remove(existingJobPosition);
                _logger.LogInformation($"Job Position Service - Job Position Id '{0}' deleted successfully.", id);
                return new JobPositionResponse(existingJobPosition);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Job Position Service - Error deleting the Job Position: {0}; Inner Exception Message: {1}", ex.Message, ex.InnerException?.Message);
                return new JobPositionResponse($"An error occurred when deleting the Job Position: {ex.Message}");
            }
        }
    }
}

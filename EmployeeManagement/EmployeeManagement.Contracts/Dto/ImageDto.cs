using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Contracts.Dto
{
    public class ImageDto
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int EmployeeId { get; set; }
    }
}

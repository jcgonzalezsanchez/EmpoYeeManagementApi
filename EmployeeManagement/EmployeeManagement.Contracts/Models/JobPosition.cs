using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EmployeeManagement.Contracts.Models
{
    public class JobPosition
    {
        [Key]
        [DataMember]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
    }
}

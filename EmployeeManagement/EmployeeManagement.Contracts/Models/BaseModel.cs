using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.Serialization;

namespace EmployeeManagement.Contracts.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }

        public BaseModel()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}

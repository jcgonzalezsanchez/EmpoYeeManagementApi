using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Contracts.Models
{
    [DataContract]
    public class Employee : BaseModel
    {
        [Key]
        [DataMember]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public int CardId { get; set; }

        [DataMember]
        [Required]
        public string FullName { get; set; }

        [DataMember]
        [SwaggerSchema(ReadOnly = true)]
        public string? UrlImage { get; set; }

        [DataMember]
        [JsonIgnore]
        public string? FileName { get; set; }

        [DataMember]
        [Required]
        public DateTime EntryDate { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("JobPosition")]
        public int JobPositionId { get; set; }
        [JsonIgnore]
        public JobPosition? JobPosition { get; set; }
    }
}

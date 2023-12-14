using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string email { get; set; }
        public string instagram { get; set; }
        [JsonIgnore]
        public PersonTypeEnum type { get; set; }
    }
}

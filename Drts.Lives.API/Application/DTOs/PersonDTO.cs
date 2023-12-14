using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PersonDTO
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 4)]
        public string name { get; set; }
        public DateTime date_of_birth { get; set; }

        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please provide a valid email")]
        public string email { get; set; }

        [Required(ErrorMessage = "instagram is required", AllowEmptyStrings = false)]
        [MinLength(4)]
        public string instagram { get; set; }

        [JsonIgnore]
        public PersonTypeEnum type { get; set; }

    }
}

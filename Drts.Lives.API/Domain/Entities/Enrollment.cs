using DataAnnotationsExtensions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Enrollment
    {
        [Key]
        [JsonIgnore]
        public int id { get; set; }

        [Required(ErrorMessage = "Live is required", AllowEmptyStrings = false)]
        public int live_id { get; set; }

        [Required(ErrorMessage = "Person registered is required", AllowEmptyStrings = false)]
        public int person_registered_id { get; set; }

        [Required(ErrorMessage = "Value is required", AllowEmptyStrings = false)]
        [Min(5)]
        public decimal value { get; set; }

        [Required(ErrorMessage = "Expiration date is required", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy hh:mm:ss")]
        public DateTime expiration_date { get; set; }

        [Required(ErrorMessage = "Payment status is required", AllowEmptyStrings = false)]
        public PaymentStatusEnum payment_status { get; set; }
    }
}

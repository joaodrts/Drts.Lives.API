using DataAnnotationsExtensions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Enrollment
    {
        [Key]
        public int id { get; set; }
        public int live_id { get; set; }
        public int person_registered_id { get; set; }
        public decimal value { get; set; }
        public DateTime expiration_date { get; set; }
        public PaymentStatusEnum payment_status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        [JsonIgnore]
        public int id { get; set; }
        public int enrollment_id { get; set; }
        [JsonIgnore]
        public DateTime payment_date { get; set; }
    }
}

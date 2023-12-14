using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Live
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int instructor_id { get; set; }
        public DateTime start_date { get; set; }
        public int duration_in_minutes { get; set; }
    }
}

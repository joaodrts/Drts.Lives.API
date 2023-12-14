﻿using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LiveDTO
    {
        [JsonIgnore]
        public int id { get; set; }

        [Required(ErrorMessage = "Title is required", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 4)]
        public string title { get; set; }

        [Required(ErrorMessage = "Description is required", AllowEmptyStrings = false)]
        [StringLength(150, MinimumLength = 10)]
        public string description { get; set; }

        [Required(ErrorMessage = "Instructor is required", AllowEmptyStrings = false)]
        public int instructor_id { get; set; }

        [Required(ErrorMessage = "Start date is required", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy hh:mm:ss")]
        public DateTime start_date { get; set; }

        [Required(ErrorMessage = "Duration is required", AllowEmptyStrings = false)]
        [Min(10)]
        public int duration_in_minutes { get; set; }
    }
}

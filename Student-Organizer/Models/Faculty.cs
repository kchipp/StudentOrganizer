using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Student_Organizer.Models
{
    public class Faculty
    {
        [Key]
        public int? Faculty_id { get; set; }
    }
}
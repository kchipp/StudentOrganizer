using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Student_Organizer.Models
{
    public class Student
    {
        [Key]
        public int? Student_id { get; set; }
    }
}
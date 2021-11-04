using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAdministration_v3.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name of Student")]
        public string StudentName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string EducationName { get; set; }
        [Required]
        [Range(1,5,ErrorMessage ="Must be within 1-5")]
        public int SemesterNo { get; set; }

    }
}

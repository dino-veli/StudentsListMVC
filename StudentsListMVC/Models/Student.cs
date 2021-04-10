using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsListMVC.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EGN { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsDeleted { get; set; }

        public string Country { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore03.Models
{
     public class Instructor
    {
        public int Id { get; set; }

        public string fullname { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? Bio { get; set; }              // optional
        public DateTime JoinedDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();













    }
}

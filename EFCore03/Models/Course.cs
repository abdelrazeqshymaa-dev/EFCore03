using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore03.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public CourseLevel Level { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        //fk
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;

        public int CategoryId { get; set; } //fk
        public Category Category { get; set; } = null!;


        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore03.Models
{
    public class Enrollment
    {

        public DateTime EnrollmentDate { get; set; }
        public int Progress { get; set; }             // 0..100
        public EnrollmentStatus Status { get; set; }
        public decimal? FinalGrade { get; set; }      // optional

        public int StudentId { get; set; }//fk
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }//fk

        public Course Course { get; set; } = null!;
    }
}
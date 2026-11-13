using EFCore03.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore03.Configration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.CourseId });   // Composite key

            builder.Property(e => e.EnrollmentDate).IsRequired();
            builder.Property(e => e.Progress).IsRequired();
            builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(e => e.FinalGrade).HasPrecision(5, 2);



            builder.HasData(
    new Enrollment
    {
        StudentId = 1,
        CourseId = 1,
        EnrollmentDate = new DateTime(2024, 7, 1),
        Progress = 40,
        Status = EnrollmentStatus.Active
    },
    new Enrollment
    {
        StudentId = 2,
        CourseId = 1,
        EnrollmentDate = new DateTime(2024, 7, 3),
        Progress = 100,
        Status = EnrollmentStatus.Completed,
        FinalGrade = 92.5m
    }
);

        }
    }
}

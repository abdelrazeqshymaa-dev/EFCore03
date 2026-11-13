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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(200);
            builder.Property(s => s.DateOfBirth).IsRequired();

            builder.HasMany(s => s.Enrollments)
                   .WithOne(e => e.Student)
                   .HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);


            builder.HasData(
    new Student
    {
        Id = 1,
        FullName = "Omar Khaled",
        Email = "omar@mail.com",
        DateOfBirth = new DateTime(2000, 5, 20)
    },
    new Student
    {
        Id = 2,
        FullName = "Mona Samir",
        Email = "mona@mail.com",
        DateOfBirth = new DateTime(2001, 8, 14)
    }
);

        }
    }
}

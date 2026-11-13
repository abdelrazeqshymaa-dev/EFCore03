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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Description).HasMaxLength(1000);
            builder.Property(c => c.Price).HasPrecision(10, 2);
            builder.Property(c => c.Level).HasConversion<string>().HasMaxLength(20); // يتخزن كنص مقروء
            builder.Property(c => c.IsPublished).IsRequired();
            

            builder.HasMany(c => c.Enrollments)
                   .WithOne(e => e.Course)
                   .HasForeignKey(e => e.CourseId)
               
                   
                   .OnDelete(DeleteBehavior.Cascade);



            builder.HasData(
new Course
{
    Id = 1,
    Title = "ASP.NET Core Basics",
    Price = 99.99m,
    Level = CourseLevel.Beginner,
    IsPublished = true,
    PublishedDate = new DateTime(2024, 6, 1),
    InstructorId = 1,
    CategoryId = 3
},
new Course
{
    Id = 2,
    Title = "Advanced EF Core",
    Price = 149.50m,
    Level = CourseLevel.Advanced,
    IsPublished = false,
    InstructorId = 1,
    CategoryId = 2
}
);







        }
    
    }
}

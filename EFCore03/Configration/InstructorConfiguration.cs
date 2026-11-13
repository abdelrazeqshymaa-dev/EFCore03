using EFCore03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EFCore03.Configration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {

            builder.HasKey(i => i.Id);
            builder.Property(i => i.fullname).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Email).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Bio).HasMaxLength(500);   // مفيش IsRequired = optional
            builder.Property(i => i.JoinedDate).IsRequired(); ;
    

            builder.HasMany(i => i.Courses)
                           .WithOne(c => c.Instructor)
                           .HasForeignKey(c => c.InstructorId).OnDelete(DeleteBehavior.Restrict); 

            builder.HasData(
    new Instructor
    {
        Id = 1,
        fullname 
 = "Ahmed Ali",
        Email = "ahmed@learn.com",
        Bio = "Backend developer",
        JoinedDate = new DateTime(2024, 1, 10)
    },
    new Instructor
    {
        Id = 2,
        fullname = "Sara Hassan",
        Email = "sara@learn.com",
        JoinedDate = new DateTime(2024, 3, 5)
    }
);
        }
    }
        }

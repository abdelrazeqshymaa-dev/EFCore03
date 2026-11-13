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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.description).HasMaxLength(500);

            // Self-referencing
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .IsRequired(false)                         // top-level مسموح
                   .OnDelete(DeleteBehavior.Restrict);        // يمنع حذف الأب اللي عنده أبناء

            builder.HasMany(c => c.Courses)
                   .WithOne(c => c.Category)
                   .HasForeignKey(c => c.CategoryId);



            builder.HasData(
    new Category { Id = 1, Name = "Programming" },
    new Category { Id = 2, Name = "Backend", ParentCategoryId = 1 },
    new Category { Id = 3, Name = "ASP.NET", ParentCategoryId = 2 }
);

        }

    }
}
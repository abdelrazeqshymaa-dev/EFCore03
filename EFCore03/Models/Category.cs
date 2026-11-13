using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore03.Models
{
    public  class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? description { get; set; }

        public int? ParentCategoryId { get; set; }   
        public Category? ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; } = new List<Category>();


        public ICollection<Course> Courses { get; set; } = new List<Course>();



    }
}

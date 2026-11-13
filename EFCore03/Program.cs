namespace EFCore03
{
    using EFCore03.Data;
    using EFCore03.Models;
    using Microsoft.EntityFrameworkCore;   

    class Program
    {
        static void Main()
        {
            // ---------- Create ----------
            AddInstructor();
            AddCourse(instructorId: 1, categoryId: 3);
            AddStudents();
            EnrollStudent(studentId: 1, courseId: 2);   
            EnrollStudent(studentId: 2, courseId: 1); 
            EnrollStudent(studentId: 99, courseId: 1); 

            // ---------- Read ----------
            FindCourse(id: 1, title: "ASP.NET Core Basics");
            FindCourse(id: 999, title: "Not A Real Course");
            FilterCourses(minPrice: 50, maxPrice: 150);
            ShowSubCategories(categoryId: 1);
            ShowTopLevelCategories();
            ShowCourseRoster(courseId: 1);

            // ---------- Update ----------
            ChangePrice(courseId: 1, newPrice: 119.99m);
            UpdateProgress(studentId: 1, courseId: 1, newProgress: 100);
            UpdateProgress(studentId: 1, courseId: 1, newProgress: 150);
            PublishCourse(courseId: 2);

            // ---------- Delete ----------
            Unenroll(studentId: 1, courseId: 1);
            DeleteCategoryUnsafe(categoryId: 1);  
            DeleteCategorySafe(categoryId: 1);  
            DeleteCourseWithEnrollments(courseId: 1);
        }

        // ===================== CREATE =====================

        static void AddInstructor()
        {
            using var db = new AppDbContext();
            var instructor = new Instructor
            {
                fullname = "Mahmoud Adel",
                Email = "mahmoud@learn.com",
                JoinedDate = DateTime.Now
            };
            db.Instructors.Add(instructor);
            db.SaveChanges();
            Console.WriteLine($"Instructor added. Id = {instructor.Id}");  
        }

        static void AddCourse(int instructorId, int categoryId)
        {
            using var db = new AppDbContext();

            if (db.Instructors.Find(instructorId) == null)
            {
                Console.WriteLine($"Instructor {instructorId} not found.");
                return;
            }
            if (db.Categories.Find(categoryId) == null)
            {
                Console.WriteLine($"Category {categoryId} not found.");
                return;
            }

            var course = new Course
            {
                Title = "Docker for Beginners",
                Price = 79.99m,
                Level = CourseLevel.Beginner,
                IsPublished = false,
                InstructorId = instructorId,     
                CategoryId = categoryId
            };
            db.Courses.Add(course);
            db.SaveChanges();
            Console.WriteLine($"Course added. Id = {course.Id}");
        }

        static void AddStudents()
        {
            using var db = new AppDbContext();
            var students = new List<Student>
        {
            new Student { FullName = "Ali Hany",   Email = "ali@mail.com",   DateOfBirth = new DateTime(2000, 1, 1) },
            new Student { FullName = "Nour Ehab",  Email = "nour@mail.com",  DateOfBirth = new DateTime(2001, 2, 2) },
            new Student { FullName = "Yara Tarek", Email = "yara@mail.com",  DateOfBirth = new DateTime(2002, 3, 3) }
        };
            db.Students.AddRange(students);  
            db.SaveChanges();                
            foreach (var s in students)
                Console.WriteLine($"Student {s.FullName} => Id = {s.Id}");
        }

        static void EnrollStudent(int studentId, int courseId)
        {
            using var db = new AppDbContext();

            var student = db.Students.Find(studentId);
            if (student == null)
            {
                Console.WriteLine($"Enrollment refused: student {studentId} not found.");
                return;
            }

            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Enrollment refused: course {courseId} not found.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine($"Enrollment refused: course '{course.Title}' is not published.");
                return;
            }

            bool alreadyEnrolled = db.Enrollments
                .Any(e => e.StudentId == studentId && e.CourseId == courseId);
            if (alreadyEnrolled)
            {
                Console.WriteLine($"Enrollment refused: {student.FullName} is already enrolled in '{course.Title}'.");
                return;
            }

            db.Enrollments.Add(new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active
            });
            db.SaveChanges();
            Console.WriteLine($"{student.FullName} enrolled in '{course.Title}'.");
        }

        // ===================== READ =====================

        static void FindCourse(int id, string title)
        {
            using var db = new AppDbContext();

            var byId = db.Courses.Find(id);                               
            Console.WriteLine(byId == null ? $"Course with Id {id}: not found"
                                           : $"Found by Id: {byId.Title}");

            var byTitle = db.Courses.FirstOrDefault(c => c.Title == title);   
            Console.WriteLine(byTitle == null ? $"Course titled '{title}': not found"
                                              : $"Found by title: {byTitle.Title}");
        }

        static void FilterCourses(decimal minPrice, decimal maxPrice)
        {
            using var db = new AppDbContext();
            var courses = db.Courses
                .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .Select(c => new { c.Title, c.Level, c.Price })   
                .ToList();

            Console.WriteLine($"Courses between {minPrice} and {maxPrice}:");
            foreach (var c in courses)
                Console.WriteLine($"  {c.Title} | {c.Level} | {c.Price}");
        }

        static void ShowSubCategories(int categoryId)
        {
            using var db = new AppDbContext();
            var subs = db.Categories.Where(c => c.ParentCategoryId == categoryId).ToList();

            Console.WriteLine($"Sub-categories of category {categoryId}:");
            foreach (var s in subs)
                Console.WriteLine($"  {s.Name}");
        }

        static void ShowTopLevelCategories()
        {
            using var db = new AppDbContext();
            var tops = db.Categories.Where(c => c.ParentCategoryId == null).ToList();

            Console.WriteLine("Top-level categories:");
            foreach (var t in tops)
                Console.WriteLine($"  {t.Name}");
        }

        static void ShowCourseRoster(int courseId)
        {
            using var db = new AppDbContext();

            // 1) الـ Ids من الـ Enrollments
            var studentIds = db.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine($"Course {courseId} has {studentIds.Count} student(s):");

        
            var students = db.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            foreach (var s in students)
                Console.WriteLine($"  {s.FullName}");
        }

        // ===================== UPDATE =====================

        static void ChangePrice(int courseId, decimal newPrice)
        {
            using var db = new AppDbContext();

            if (newPrice <= 0)
            {
                Console.WriteLine("Invalid price: it must be greater than 0.");
                return;
            }

            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Course {courseId} not found.");
                return;
            }

            course.Price = newPrice;   
            db.SaveChanges();
            Console.WriteLine($"Price of '{course.Title}' changed to {newPrice}.");
        }

        static void UpdateProgress(int studentId, int courseId, int newProgress)
        {
            using var db = new AppDbContext();

            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine($"Invalid progress {newProgress}: it must be between 0 and 100.");
                return;
            }

            var enrollment = db.Enrollments.Find(studentId, courseId);   // الترتيب زي الـ HasKey
            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            enrollment.Progress = newProgress;
            if (newProgress == 100)
                enrollment.Status = EnrollmentStatus.Completed;

            db.SaveChanges();
            Console.WriteLine($"Progress updated to {newProgress}. Status: {enrollment.Status}");
        }

        static void PublishCourse(int courseId)
        {
            using var db = new AppDbContext();
            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Course {courseId} not found.");
                return;
            }

            course.IsPublished = true;
            course.PublishedDate = DateTime.Now;
            db.SaveChanges();
            Console.WriteLine($"Course '{course.Title}' published on {course.PublishedDate}.");
        }

        // ===================== DELETE =====================

        static void Unenroll(int studentId, int courseId)
        {
            using var db = new AppDbContext();
            var enrollment = db.Enrollments.Find(studentId, courseId);
            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            Console.WriteLine("Student unenrolled.");
        }

        static void DeleteCategoryUnsafe(int categoryId)
        {
            using var db = new AppDbContext();
            var category = db.Categories.Find(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();     // هتفشل بسبب DeleteBehavior.Restrict
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("DbUpdateException: " + ex.Message);
                Console.WriteLine("Inner: " + ex.InnerException?.Message);   // دي اللي فيها السبب الحقيقي
            }
        }

        static void DeleteCategorySafe(int categoryId)
        {
            using var db = new AppDbContext();
            var category = db.Categories.Find(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            bool hasSubs = db.Categories.Any(c => c.ParentCategoryId == categoryId);
            if (hasSubs)
            {
                Console.WriteLine($"Cannot delete '{category.Name}': it still has sub-categories.");
                return;
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            Console.WriteLine($"Category '{category.Name}' deleted.");
        }

        static void DeleteCourseWithEnrollments(int courseId)
        {
            using var db = new AppDbContext();
            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            int before = db.Enrollments.Count(e => e.CourseId == courseId);
            Console.WriteLine($"Enrollments before delete: {before}");

            db.Courses.Remove(course);
            db.SaveChanges();

            int after = db.Enrollments.Count(e => e.CourseId == courseId);
            Console.WriteLine($"Enrollments after delete: {after}");

    
        }
    }
}

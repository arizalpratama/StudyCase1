using EnrollmentService.Models;
using System;
using System.Linq;

namespace EnrollmentService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{FirstName ="Erick",LastName="Kurniawan",EnrollmentDate=DateTime.Parse("2021-12-12")},
                new Student{FirstName ="Agus",LastName="Kurniawan",EnrollmentDate=DateTime.Parse("2021-12-12")},
                new Student{FirstName ="Peter",LastName="Parker",EnrollmentDate=DateTime.Parse("2021-12-12")},
                new Student{FirstName ="Tony",LastName="Stark",EnrollmentDate=DateTime.Parse("2021-12-12")},
                new Student{FirstName ="Bruce",LastName="Banner",EnrollmentDate=DateTime.Parse("2021-12-12")}
            };

            foreach (var s in students)
            {
                context.Students.Add(s);
            }

            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{Title ="Cloud Fundamentals",Description="Clouds is desc."},
                new Course{Title ="Microservices Architecture",Description="Microservices is desc."}, 
                new Course{Title ="Fronted Programming",Description="Frontend is.."},
                new Course{Title ="Backend RESTful API",Description="Backend Restful API is desc."},
                new Course{Title ="Entity Framework Core",Description="Entity framework is desc."}
            };

            foreach (var c in courses)
            {
                context.Courses.Add(c);
            }

            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1},
                new Enrollment{StudentID=1,CourseID=2},
                new Enrollment{StudentID=1,CourseID=3},
                new Enrollment{StudentID=2,CourseID=1},
                new Enrollment{StudentID=2,CourseID=2},
                new Enrollment{StudentID=2,CourseID=3},
                new Enrollment{StudentID=3,CourseID=1},
                new Enrollment{StudentID=3,CourseID=2},
                new Enrollment{StudentID=3,CourseID=3}
            };

            foreach (var e in enrollments)
            {
                context.Enrollments.Add(e);
            }

            context.SaveChanges();
        }
    }
}

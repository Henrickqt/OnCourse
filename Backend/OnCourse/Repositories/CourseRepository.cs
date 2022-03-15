using Microsoft.EntityFrameworkCore;
using OnCourse.Data;
using OnCourse.Enums;
using OnCourse.Models;
using OnCourse.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnCourse.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OnCourseContext _context;

        public CourseRepository(OnCourseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(EnCourseStatus? status)
        {
            return status == null
                ? await _context.Courses.ToListAsync()
                : await _context.Courses.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<Course> GetCourseAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<bool> PutCourseAsync(int id, Course course)
        {
            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Course> PostCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}

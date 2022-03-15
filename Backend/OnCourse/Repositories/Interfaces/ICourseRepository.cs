using OnCourse.Enums;
using OnCourse.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnCourse.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync(EnCourseStatus? status);

        Task<Course> GetCourseAsync(int id);
        
        Task<bool> PutCourseAsync(int id, Course course);

        Task<Course> PostCourseAsync(Course course);

        Task<bool> DeleteCourseAsync(int id);
    }
}

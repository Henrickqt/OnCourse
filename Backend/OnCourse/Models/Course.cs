using OnCourse.Enums;

namespace OnCourse.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public int Duration { get; set; }
        
        public EnCourseStatus Status { get; set; }
    }
}

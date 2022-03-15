using OnCourse.Enums;

namespace OnCourse.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public EnUserRole Role { get; set; }
    }
}

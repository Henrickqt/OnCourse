using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnCourse.Enums;
using OnCourse.Models;
using OnCourse.Repositories.Interfaces;

namespace OnCourse.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _repository;

        public CoursesController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses([FromQuery] EnCourseStatus? status)
        {
            var courses = await _repository.GetCoursesAsync(status);
            
            if (courses == null || !courses.Any())
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse([FromRoute] int id)
        {
            var course = await _repository.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [Authorize(Roles = "Manager, Secretary")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse([FromRoute] int id, [FromBody] Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            var result = await _repository.PutCourseAsync(id, course);

            if (result == false)
            {
                return NotFound();
            }
                
            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromBody] Course course)
        {
            var result = await _repository.PostCourseAsync(course);

            return CreatedAtAction("GetCourse", new { id = result.Id }, result);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            var result = await _repository.DeleteCourseAsync(id);

            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

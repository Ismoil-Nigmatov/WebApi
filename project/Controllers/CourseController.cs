
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : AppDbController<Course, CourseRepository>
    {
        public CourseController(CourseRepository repository) : base(repository) { }
    }
}

using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : AppDbController<Teacher, TeacherRepository>
    {
        public TeacherController(TeacherRepository repository) : base(repository) { }
    }
}


using Microsoft.AspNetCore.Mvc;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : AppDbController<Test, TestRepository>
    {
        public TestController(TestRepository testRepository) : base(testRepository) { }
    }
}

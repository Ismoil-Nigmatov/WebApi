using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using project.Dto;
using project.Entity;
using project.repository;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController :ControllerBase
    {
        private readonly ITestRepository _testRepository;

        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTests()
        {
            var tests = await _testRepository.GetAllTestAsync();
            return Ok(tests ?? new List<Test>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTestById(int id)
        {
            var test = await _testRepository.GetTestByIdAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteTest(int id)
        {
            await _testRepository.DeleteTestAsync(id);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> AddTest(TestDTO testDto)
        {
            await _testRepository.AddTestAsync(testDto);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTest(TestDTO testDto)
        {
            await _testRepository.UpdateTestAsync(testDto);
            return Ok();
        }
    }
}

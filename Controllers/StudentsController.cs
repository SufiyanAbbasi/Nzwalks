using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nzwalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Doe", "Bobs", "harry" };
            return Ok(studentNames);
        }
    }
}

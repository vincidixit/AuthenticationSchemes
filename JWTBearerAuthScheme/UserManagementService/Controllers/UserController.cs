using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{username}")]
        public IActionResult GetUserDetails(string username)
        {
            // Return dummy user details
            return Ok(new { Name = "Miguel O'hara", Location = "Pune, MH, India" });
        }
    }
}

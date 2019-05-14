using HomeSwitchHome.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeSwitchHome.API.Controllers.V1
{
    public class HomeController : ControllerBase
    {
        [HttpGet("api/test/test-anon")]
        public IActionResult TestAnonymous()
        {
            return Ok("Anonymous");
        }

        [HttpGet("api/test/test-basic")]
        [Authorize(Roles = UserRole.Basic)]
        public IActionResult TestBasic()
        {
            return Ok("Basic");
        }


        [HttpGet("api/test/test-admin")]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult TestAdmin()
        {
            return Ok("Admin");
        }

        [HttpGet("api/test/test-premium")]
        [Authorize(Roles = UserRole.Premium)]
        public IActionResult TestPremium()
        {
            return Ok("Premium");
        }
    }
}
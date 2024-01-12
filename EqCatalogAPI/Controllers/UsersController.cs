using EqCatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EqCatalogAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EqCatalogContext _context;

        public UsersController(EqCatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetUser(string login, string pass)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == pass);

            if (user == null)
            {
                return NotFound();
            }

            return user.FullName;
        }
    }
}

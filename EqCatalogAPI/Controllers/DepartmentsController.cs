using EqCatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EqCatalogAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		private readonly EqCatalogContext _context;

		public DepartmentsController(EqCatalogContext context)
		{
			_context = context;
		}

		// GET: api/Departments
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
		{
			return await _context.Departments.ToListAsync();
		}

		[HttpGet("{department}")]
		public async Task<ActionResult<IEnumerable<Place>>> GetDepartments(string department)
		{
			List<Place> data = await _context.Places.Where(x => x.Department.Name == department).ToListAsync();

			if (data.IsNullOrEmpty())
			{
				return NotFound();
			}

			return data;
		}
	}
}

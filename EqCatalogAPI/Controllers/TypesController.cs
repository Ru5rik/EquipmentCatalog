using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EqCatalogAPI.Models;

namespace EqCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly EqCatalogContext _context;

        public TypesController(EqCatalogContext context)
        {
            _context = context;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Type>>> GetTypes()
        {
            return await _context.Types.ToListAsync();
        }

    }
}

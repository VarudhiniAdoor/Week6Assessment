using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeptApi.Models;

namespace DeptApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly DeptDbContext _context;

        public ManagerController(DeptDbContext context)
        {
            _context = context;
        }

        // GET: api/Manager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manager>>> GetManagers()
        {
            return await _context.Managers.Include(m => m.Departments).ToListAsync();
        }

        // GET: api/Manager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id)
        {
            var manager = await _context.Managers
                                        .Include(m => m.Departments)
                                        .FirstOrDefaultAsync(m => m.ManagerId == id);

            if (manager == null)
            {
                return NotFound();
            }

            return manager;
        }

        // POST: api/Manager
        [HttpPost]
        public async Task<ActionResult<Manager>> PostManager(Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetManager), new { id = manager.ManagerId }, manager);
        }

        // PUT: api/Manager/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManager(int id, Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return BadRequest();
            }

            _context.Entry(manager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Managers.Any(e => e.ManagerId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Manager/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

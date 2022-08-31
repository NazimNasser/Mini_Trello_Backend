using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly Context _context;

        public StatusesController(Context context)
        {
            _context = context;
        }

        // GET: api/Statuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
          if (_context.Statuses == null)
          {
              return NotFound();
          }
            return await _context.Statuses.ToListAsync();
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
          if (_context.Statuses == null)
          {
              return NotFound();
          }
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // PUT: api/Statuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutStatus(int id, Status model)
        {
            // Use of lambda expression to access
            // particular record from a database
            var data = _context.Statuses.FirstOrDefault(x => x.StatusId == id);
            // Checking if any such record exist
            if (data != null)
            {
                data.Name = model.Name;
                _context.SaveChanges();
                // It will redirect to
                // the Read method
                return Ok("Success!");
            }
            else
                return Ok("The Status not found");
        }

        // POST: api/Statuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
          if (_context.Statuses == null)
          {
              return Problem("Entity set 'Context.Statuses'  is null.");
          }
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetStatus", new { id = status.StatusId }, status);
            return CreatedAtAction(nameof(GetStatus), new { id = status.StatusId }, status);
        }

        // DELETE: api/Statuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            if (_context.Statuses == null)
            {
                return NotFound();
            }
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return (_context.Statuses?.Any(e => e.StatusId == id)).GetValueOrDefault();
        }
    }
}

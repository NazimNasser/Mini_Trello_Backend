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
    public class CardsController : ControllerBase
    {
        private readonly Context _context;

        public CardsController(Context context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Card>>> GetCard(int id)
        {
          if (_context.Cards == null)
          {
              return NotFound();
          }
            var card = await _context.Cards
            .Where(u=> u.UserId == id)
            .Include(c=>c.User)
            .Include(c=>c.Status)
            .ToListAsync();

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutCard(int id, CardDto model)
        {
            // Use of lambda expression to access
            // particular record from a database
            var data = _context.Cards.FirstOrDefault(x => x.CardId == id);
            // Checking if any such record exist
            if (data != null)
            {
                data.Title = model.Title;
                data.Category = model.Category;
                data.DueTime = model.DueTime;
                data.EstimateCount = model.EstimateCount;
                data.EstimateUnit = model.EstimateUnit;
                data.Importance = model.Importance;
                data.StatusId = model.StatusId;
                data.UserId = model.UserId;
                _context.SaveChanges();
                // It will redirect to
                // the Read method
                return Ok("Success!");
            }
            else
                return Ok("The card not found");
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Card>>> CreateCard(CardDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound();

            var status = await _context.Statuses.FindAsync(request.StatusId);
            if (status == null)
                return NotFound();

            var newCard = new Card
            {
                Title = request.Title,
                Category = request.Category,
                DueTime = request.DueTime,
                EstimateCount = request.EstimateCount,
                EstimateUnit = request.EstimateUnit,
                Importance = request.Importance,
                Status = status,
                User = user
            };

            _context.Cards.Add(newCard);
            await _context.SaveChangesAsync();
            return await GetCard(newCard.UserId);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return (_context.Cards?.Any(e => e.CardId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GatheringTheMagic.Models;
using GatheringTheMagic.Client.Models;
using System.Text;

namespace GatheringTheMagic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly MtgcardsContext _context;
        

        public CardsController(MtgcardsContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<PagedResponse<Card>>> GetCards([FromQuery] PaginationParams paginationParams, string? colorFilter = null, string? setFilter = null)
        {

            var query = _context.Cards
                .Include(c => c.CardColors)
                .AsQueryable();

            if (!string.IsNullOrEmpty(colorFilter))
            {
                if (colorFilter == "C") // Colorless
                {
                    query = query.Where(c => !c.CardColors.Any());
                }
                else if (colorFilter == "multicolor")
                {
                    query = query.Where(c => c.CardColors.Count() > 1);
                }
                else
                {
                    query = query.Where(c => c.CardColors.Any(col => col.Color == colorFilter));
                }
            }

            if (!string.IsNullOrEmpty(setFilter)) 
            {
                query = query.Where(c => c.SetCode == setFilter);
            }

            var totalRecords = await query.CountAsync();
            var cards = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            var pagedResponse = new PagedResponse<Card>(cards, paginationParams.PageNumber, paginationParams.PageSize, totalRecords);

            return Ok(pagedResponse);
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(string id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(string id, NewCard card)
        {
            Card cardToEdit = new Card(card);
            if (id != cardToEdit.Id)
            {
                return BadRequest();
            }
            if (cardToEdit.SetCode != "HBR")
            {
                return BadRequest("Cannot change non-homebrew cards");
            }

            _context.Entry(cardToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(NewCard card)
        {

            Card cardToAdd = new Card(card);

            _context.Cards.Add(cardToAdd);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(string id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            if (card.SetCode != "HBR")
            {
                return BadRequest("Cannot delete non-homebrew cards");
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(string id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }

    }
}

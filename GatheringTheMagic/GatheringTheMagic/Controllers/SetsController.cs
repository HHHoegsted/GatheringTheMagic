using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GatheringTheMagic.Models;
using Microsoft.EntityFrameworkCore;

namespace GatheringTheMagic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly MtgcardsContext _context;

        public SetsController(MtgcardsContext context) 
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardSetDTO>>> GetSets()
        {
            var query = await _context.Cards
                .Where(c => c.SetCode != null && c.SetName != null)
                .Select(c => new CardSetDTO
                {
                    SetCode = c.SetCode!,
                    SetName = c.SetName!
                })
                .Distinct()
                .OrderBy(c => c.SetCode)
                .ToListAsync();

            return Ok(query);
        }
    }
}

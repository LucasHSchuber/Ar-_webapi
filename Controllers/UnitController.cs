using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aråstock.Models;

namespace Aråstock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly StockDbContext _context;

        public UnitController(StockDbContext context)
        {
            _context = context;
        }

        // GET: api/Unit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnit()
        {
            return await _context.Unit.ToListAsync();
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var unit = await _context.Unit.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Unit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(int id, Unit unit)
        {
            if (id != unit.UnitID)
            {
                return BadRequest();
            }

            var existingUnit = await _context.Unit.FindAsync(id);
            if (existingUnit == null){
                return NotFound();
            }

            existingUnit.UnitName = unit.UnitName;
            // _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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



        // POST: api/Unit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {

            var existingUnit = _context.Unit.FirstOrDefault(i => i.UnitName.ToLower() == unit.UnitName.ToLower());

            if (existingUnit != null)
            {
                return Conflict("Det finns redan en enhet med det namet");
            }

            _context.Unit.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.UnitID }, unit);
        }



        // DELETE: api/Unit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _context.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }


            var deletedUnits = await _context.Item
                .Where(r => r.UnitID == id)
                .ToListAsync();

            _context.Item.RemoveRange(deletedUnits);
            _context.Unit.Remove(unit);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(int id)
        {
            return _context.Unit.Any(e => e.UnitID == id);
        }
    }
}

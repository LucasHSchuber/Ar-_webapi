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
    public class InformationController : ControllerBase
    {
        private readonly StockDbContext _context;

        public InformationController(StockDbContext context)
        {
            _context = context;
        }

        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Information>>> GetInformation()
        {
            var information = await _context.Information
                .OrderByDescending(i => i.Created)
                .ToListAsync(); // Use ToListAsync to execute the query asynchronously

            return Ok(information); // Wrap the result in an Ok ActionResult
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Information>> GetInformation(int id)
        {
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return NotFound();
            }

            return information;
        }



        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, Information information)
        {
            if (id != information.Id)
            {
                return BadRequest();
            }

            _context.Entry(information).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationExists(id))
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




        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Information>> PostInformation(Information information)
        {
            information.Created = DateTime.Now;
            _context.Information.Add(information);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformation", new { id = information.Id }, information);
        }




        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformationExists(int id)
        {
            return _context.Information.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aråstock.Models;
using Aråstock.DTOs;


namespace Aråstock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly StockDbContext _context;

        public ItemController(StockDbContext context)
        {
            _context = context;
        }



        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItem()
        {
            return await _context.Item
                .Select(item => new ItemDto
                {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    CategoryID = item.CategoryID,
                    CategoryName = item.Category.CategoryName, // Include only necessary Category properties
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    TotalAmountInStock = item.TotalAmountInStock,
                    UnitID = item.UnitID,
                    UnitName = item.Unit.UnitName,
                    Created = item.Created
                    // Add other properties as needed
                })
                .ToListAsync();
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Item.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // GET: api/Item/today
        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemToday()
        {
            DateTime today = DateTime.Today;

            return await _context.Item
                .Where(item => item.Created.Date == today.Date)
                .OrderByDescending(item => item.Created)
                .Select(item => new ItemDto
                {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    CategoryID = item.CategoryID,
                    CategoryName = item.Category.CategoryName,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    TotalAmountInStock = item.TotalAmountInStock,
                    UnitID = item.UnitID,
                    UnitName = item.Unit.UnitName,
                    Created = item.Created
                })
                .ToListAsync();
        }

        // GET: api/Item/search/searchString
        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemSearch(string searchString)
        {

            return await _context.Item
                .Where(item => item.ItemName.ToLower().Contains(searchString.ToLower()))
                .Select(item => new ItemDto
                {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    CategoryID = item.CategoryID,
                    CategoryName = item.Category.CategoryName,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    TotalAmountInStock = item.TotalAmountInStock,
                    UnitID = item.UnitID,
                    UnitName = item.Unit.UnitName,
                    Created = item.Created
                })
                .ToListAsync();
        }




        // PUT: api/Item/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, [FromBody] Item item)
        {
            if (id != item.ItemID)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // PUT: api/Item/stockupdate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("updatestock/{id}")]
        public async Task<IActionResult> UpdateStock(int id, int amount)
        {

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Amount = amount;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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



        // POST: api/Item
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {

            var existingItem = await _context.Item.FirstOrDefaultAsync(i => i.ItemName == item.ItemName);

            if (existingItem != null)
            {
                // If an item with the same itemName already exists, return a conflict response
                return Conflict("Det finns redan en vara med det namet");
            }


            item.Created = DateTime.Now;
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ItemID }, item);
        }





        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemID == id);
        }
    }
}

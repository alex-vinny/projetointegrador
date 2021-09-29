using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Data;
using ProjetoIntegrador.Api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensCruzadaController : ControllerBase
    {
        private readonly BancoContext _context;

        public ItensCruzadaController(BancoContext context)
        {
            _context = context;
        }

        // GET: api/ItensCruzada
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CruzadaItem>>> GetCruzadaItens()
        {
            return await _context.CruzadaItens.ToListAsync();
        }

        // GET: api/ItensCruzada/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CruzadaItem>> GetCruzadaItem(int id)
        {
            var cruzadaItem = await _context.CruzadaItens.FindAsync(id);

            if (cruzadaItem == null)
            {
                return NotFound();
            }

            return cruzadaItem;
        }

        // PUT: api/ItensCruzada/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCruzadaItem(int id, CruzadaItem cruzadaItem)
        {
            if (id != cruzadaItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(cruzadaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CruzadaItemExists(id))
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

        // POST: api/ItensCruzada
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CruzadaItem>> PostCruzadaItem(CruzadaItem cruzadaItem)
        {
            _context.CruzadaItens.Add(cruzadaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCruzadaItem", new { id = cruzadaItem.ID }, cruzadaItem);
        }

        // DELETE: api/ItensCruzada/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCruzadaItem(int id)
        {
            var cruzadaItem = await _context.CruzadaItens.FindAsync(id);
            if (cruzadaItem == null)
            {
                return NotFound();
            }

            _context.CruzadaItens.Remove(cruzadaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CruzadaItemExists(int id)
        {
            return _context.CruzadaItens.Any(e => e.ID == id);
        }
    }
}

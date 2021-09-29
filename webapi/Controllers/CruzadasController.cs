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
    public class CruzadasController : ControllerBase
    {
        private readonly BancoContext _context;

        public CruzadasController(BancoContext context)
        {
            _context = context;
        }

        // GET: api/Cruzadas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cruzada>>> GetCruzadas()
        {
            return await _context.Cruzadas.ToListAsync();
        }

        // GET: api/Cruzadas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cruzada>> GetCruzada(int id)
        {
            var cruzada = await _context.Cruzadas.FindAsync(id);

            if (cruzada == null)
            {
                return NotFound();
            }

            return cruzada;
        }

        // PUT: api/Cruzadas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCruzada(int id, Cruzada cruzada)
        {
            if (id != cruzada.ID)
            {
                return BadRequest();
            }

            _context.Entry(cruzada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CruzadaExists(id))
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

        // POST: api/Cruzadas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cruzada>> PostCruzada(Cruzada cruzada)
        {
            _context.Cruzadas.Add(cruzada);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCruzada", new { id = cruzada.ID }, cruzada);
        }

        // DELETE: api/Cruzadas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCruzada(int id)
        {
            var cruzada = await _context.Cruzadas.FindAsync(id);
            if (cruzada == null)
            {
                return NotFound();
            }

            _context.Cruzadas.Remove(cruzada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CruzadaExists(int id)
        {
            return _context.Cruzadas.Any(e => e.ID == id);
        }
    }
}

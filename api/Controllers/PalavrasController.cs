using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegrador.Api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalavrasController : ControllerBase
    {
        private readonly BancoContext _context;

        public PalavrasController(BancoContext context)
        {
            _context = context;
        }

        // GET: api/Palavras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Palavra>>> GetPalavras()
        {
            return await _context.Palavras.ToListAsync();
        }

        // GET: api/Palavras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Palavra>> GetPalavra(int id)
        {
            var palavra = await _context.Palavras.FindAsync(id);

            if (palavra == null)
            {
                return NotFound();
            }

            return palavra;
        }

        // PUT: api/Palavras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPalavra(int id, Palavra palavra)
        {
            if (id != palavra.ID)
            {
                return BadRequest();
            }

            _context.Entry(palavra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PalavraExists(id))
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

        // POST: api/Palavras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Palavra>> PostPalavra(Palavra palavra)
        {
            _context.Palavras.Add(palavra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPalavra", new { id = palavra.ID }, palavra);
        }

        // DELETE: api/Palavras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePalavra(int id)
        {
            var palavra = await _context.Palavras.FindAsync(id);
            if (palavra == null)
            {
                return NotFound();
            }

            _context.Palavras.Remove(palavra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PalavraExists(int id)
        {
            return _context.Palavras.Any(e => e.ID == id);
        }
    }
}

using _06_WebApi.Interfaces;
using _06_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _06_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private IGeneric _api;
        public GenreController(IGeneric api)
        {
            this._api = api;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M_Genre>>> GetGenres()
        {
            return (await _api.Get<M_Genre>()).ToList();
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M_Genre>> GetGenre(int id)
        {

            var m_Genre = await _api.GetFirstAsync<M_Genre, int>("Id", id);

            if (m_Genre == null)
            {
                return NotFound();
            }

            return m_Genre;
        }

        // PUT: api/Genre/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, M_Genre m_Genre)
        {
            if (id != m_Genre.Id)
            {
                return BadRequest();
            }

            try
            {
                await _api.Update<M_Genre>(id, m_Genre);
                return Ok(m_Genre);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Genre
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<M_Genre>> PostGenre(M_Genre m_Genre)
        {
            await _api.Add(m_Genre);

            return CreatedAtAction("GetGenre", new { id = m_Genre.Id }, m_Genre);
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var m_Genre = await _api.GetFirstAsync<M_Genre, int>("Id", id);
            if (m_Genre == null)
            {
                return NotFound();
            }
            await _api.Delete<M_Genre>(id);

            return NoContent();
        }

        private async Task<bool> GenreExists(int id)
        {
            var m_Genre = await _api.GetFirstAsync<M_Genre, int>("Id", id);
            return m_Genre != null;
        }
    }
}

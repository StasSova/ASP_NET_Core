using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _06_WebApi;
using _06_WebApi.Models;
using _06_WebApi.Interfaces;

namespace _06_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private IGeneric _api;
        public ArtistController(IGeneric api)
        {
            this._api = api;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M_Artist>>> GetArtists()
        {
            return (await _api.Get<M_Artist>()).ToList();
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M_Artist>> GetArtist(int id)
        {

            var m_Artist = await _api.GetFirstAsync<M_Artist, int>("Id", id);

            if (m_Artist == null)
            {
                return NotFound();
            }

            return m_Artist;
        }

        // PUT: api/Artist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, M_Artist m_Artist)
        {
            if (id != m_Artist.Id)
            {
                return BadRequest();
            }

            try
            {
                await _api.Update<M_Artist>(id, m_Artist);
                return Ok(m_Artist);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<M_Artist>> PostArtist(M_Artist m_Artist)
        {
            await _api.Add(m_Artist);

            return CreatedAtAction("GetArtist", new { id = m_Artist.Id }, m_Artist);
        }

        // DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var m_Artist = await _api.GetFirstAsync<M_Artist, int>("Id", id);
            if (m_Artist == null)
            {
                return NotFound();
            }
            await _api.Delete<M_Artist>(id);

            return NoContent();
        }

        private async Task<bool> ArtistExists(int id)
        {
            var m_Artist = await _api.GetFirstAsync<M_Artist, int>("Id", id);
            return m_Artist != null;
        }
    }
}

using _06_WebApi.Interfaces;
using _06_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _06_WebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IGeneric _api;
        public UserController(IGeneric api)
        {
            this._api = api;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M_User>>> GetUsers()
        {
            return (await _api.Get<M_User>()).ToList();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M_User>> GetUser(int id)
        {

            var m_User = await _api.GetFirstAsync<M_User, int>("Id", id);

            if (m_User == null)
            {
                return NotFound();
            }

            return m_User;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, M_User m_User)
        {
            if (id != m_User.Id)
            {
                return BadRequest();
            }

            try
            {
                m_User.Salt = "testSalt";
                await _api.Update<M_User>(id, m_User);
                return Ok(m_User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<M_User>> PostUser(M_User m_User)
        {
            await _api.Add(m_User);
            return CreatedAtAction("GetUser", new { id = m_User.Id }, m_User);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var m_User = await _api.GetFirstAsync<M_User, int>("Id", id);
            if (m_User == null)
            {
                return NotFound();
            }
            await _api.Delete<M_User>(id);

            return NoContent();
        }

        private async Task<bool> UserExists(int id)
        {
            var m_User = await _api.GetFirstAsync<M_User, int>("Id", id);
            return m_User != null;
        }
    }
}

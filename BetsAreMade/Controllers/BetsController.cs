using BetsAreMade.DataContracts.Dto.Users;
using BetsAreMade.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BetsAreMade.Controllers
{
    [ApiController]
    [Route("api")]

    public class BetsController : ControllerBase
    {
        private readonly UserService _service;

        public BetsController(UserService service)
        {
            _service = service;
        }

        [HttpGet("users/{userId}/bets")]
        public async Task<ActionResult<UserDto>> Get(string userId)
        {
            var user = await _service.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user.Bets);
        }

        [HttpGet("users/{userId}/bets/{id}")]
        public async Task<ActionResult<UserDto>> GetById(string userId, string id)
        {
            var user = await _service.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var bet = user.Bets.FirstOrDefault(b => b.Id == id);

            if (bet is null)
            {
                return NotFound("Bet not found");
            }

            return Ok(bet);
        }

        [HttpPost("users/{userId}/bets")]
        public async Task<IActionResult> Post(string userId, [FromBody] BetDto bet)
        {
            var user = await _service.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound("User not found");
            }

            user.AddBets(bet);

            await _service.UpdateAsync(user);

            return Ok(bet);
        }

        [HttpDelete("users/{userId}/bets/{id}")]
        public async Task<IActionResult> Delete(string userId, string id)
        {
            var user = await _service.GetByIdAsync(userId);

            if (user is null)
            {
                return NotFound();
            }

            var bet = user.Bets.FirstOrDefault(b => b.Id == id);

            if (bet is null)
            {
                return NoContent();
            }

            user.RemoveBet(bet);

            await _service.UpdateAsync(user);

            return Ok();
        }
    }
}

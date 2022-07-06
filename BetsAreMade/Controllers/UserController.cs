using BetsAreMade.DataContracts.Dto.Users;
using BetsAreMade.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetsAreMade.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        private readonly ILogger _logger;

        public UserController(UserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            _logger.LogWarning($"Getting all users");

            return Ok(await _service.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(string id)
        {
            _logger.LogWarning($"Getting user by id: {id}");

            var user = await _service.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserDto user)
        {
            _logger.LogWarning($"Post user");

            return Ok(await _service.CreateAsync(user));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]string id, [FromBody]UserDto user)
        {
            _logger.LogWarning($"Update user by id: {id}");

            var existedUser = await _service.GetByIdAsync(id);

            if (existedUser is null)
            {
                return NotFound();
            }

            user.Id = existedUser.Id;

            await _service.UpdateAsync(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogWarning($"Delete user by id: {id}");

            var user = await _service.GetByIdAsync(id);

            if (user is null)
            {
                return NoContent();
            }

            await _service.DeleteAsync(id);

            return Ok();
        }
    }
}

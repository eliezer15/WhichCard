using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhichCard.Services;
using WhichCard.Entities;

namespace WhichCard.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                return NotFound(id);
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]User user)
        {
            var updatedUser = await _userService.UpdateAsync(id, user);
			if (updatedUser == null)
			{
				return NotFound(id);
			}

			return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userToDelete = await _userService.GetAsync(id);
            if (userToDelete == null)
            {
                return NotFound(id);
            }

            await _userService.DeleteAsync(userToDelete);
            return NoContent(); 
        }
    }
}

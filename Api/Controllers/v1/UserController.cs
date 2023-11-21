using Api.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userServices.GetAllUsers();
        }
        [HttpGet("{id}")]
        public ActionResult<List<User>> GetSingleUser(int id)
        {
            var singleUser = _userServices.GetSingleUser(id);
            if (singleUser == null)
                return NotFound("User not found.");
            return Ok(singleUser);
        }
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            try
            {
                var singleUser = await _userServices.AddUserAsync(user);
                if (singleUser == null)
                    return NotFound("User not found.");
                return Ok(singleUser);
            }
            catch (Exception)
            {
                return StatusCode(400, "Bed request");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User user)
        {
            try
            {
                var singleUser = await _userServices.UpdateUserAsync(id, user);
                if (singleUser == null)
                    return NotFound("User not found.");

                return Ok(singleUser);
            }
            catch (Exception)
            {
                return StatusCode(400, "Bed request");
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var result = await _userServices.DeleteUserAsync(id);
            if (result == null)
                return NotFound("User not found.");

            return Ok(result);
        }
    }
}


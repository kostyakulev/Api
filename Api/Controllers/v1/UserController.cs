using Api.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>             
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)] // Specifies the data type for a successful response
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userServices.GetAllUsers();
        }
        /// <summary>
        /// Get information about a specific user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>Returns information about a specific user.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        public ActionResult<User> GetSingleUser(int id)
        {
            var singleUser = _userServices.GetSingleUser(id);
            if (singleUser == null)
                return NotFound("User not found.");
            return Ok(singleUser);
        }
        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">Data for the new user.</param>
        /// <returns>Returns information about the added user.</returns>
           
        [HttpPost]
        [ProducesResponseType(typeof(User), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                var singleUser = await _userServices.AddUserAsync(user);
                if (singleUser == null)
                    return NotFound("User not found.");
                return Ok(singleUser);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bed request {ex.Message}");
            }
        }
        /// <summary>
        /// Update information about a user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user to update.</param>
        /// <param name="user">New data for updating the user.</param>
        /// <returns>Returns information about the updated user.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            try
            {
                var singleUser = await _userServices.UpdateUserAsync(id, user);
                if (singleUser == null)
                    return NotFound("User not found.");

                return Ok(singleUser);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bed request{ex.Message}");
            }

        }
        /// <summary>
        /// Delete a user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user to delete.</param>
        /// <returns>Returns information about the deleted user.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        public async Task<ActionResult> DeleteUser(int id)
        {
            try {
                var result = await _userServices.DeleteUserAsync(id);
                if (result == false)
                    return NotFound("User not found.");

                return Ok();
            } catch(Exception ex) 
            {
                return BadRequest(ex);
            }
            
        }
    }
}


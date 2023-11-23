using Api.Services.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    //[Authorize]
    public class UserControllerHttp : ControllerBase
    {
        
        private readonly IUserServicesHttp _userServicesHttp;

        public UserControllerHttp(IUserServicesHttp userServicesHttp)
        {
            _userServicesHttp = userServicesHttp ?? throw new ArgumentNullException(nameof(userServicesHttp));
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Returns a list of all users.</returns>             
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)] // Specifies the data type for a successful response
        public async Task<ActionResult<List<UserHttp>>> GetAllUsers()
        {
            try
            {
                
                var users = await _userServicesHttp.GetAllUsers();
                return Ok(users);
               
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get information about a specific user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>Returns information about a specific user.</returns>
        /// <remarks>
        /// Example successful response:
        /// 
        /// {
        ///     "userId": 1,
        ///     "userName": "Example User"
        /// }
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<User>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        public async Task<ActionResult<UserHttp>> GetSingleUser(int id)
        {
            try
            {
                var user = await _userServicesHttp.GetSingleUser(id);
                if (user == null)
                {
                    
                    return NotFound($"User with ID {id} not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="user">Data for the new user.</param>
        /// <returns>Returns information about the added user.</returns>
        /// <remarks>
        /// Example successful response:
        /// 
        /// {
        ///     "userId": 1,
        ///     "userName": "New User"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(List<User>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<List<UserHttp>>> AddUser([FromBody] UserHttp userhttp)
        {
            try
            {
                var users = await _userServicesHttp.AddUserAsync(userhttp);
                return Ok(users);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update information about a user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user to update.</param>
        /// <param name="userhttp">New data for updating the user.</param>
        /// <returns>Returns information about the updated user.</returns>
        /// <response code="200">Successful update. Returns information about the updated user.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpPut("{id}", Name = "updateUser")]
        [ProducesResponseType(typeof(List<UserHttp>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        [ProducesResponseType(500)] // Specifies internal server error
        public async Task<ActionResult<List<UserHttp>>> UpdateUser(int id, [FromBody] UserHttp userhttp)
        {
            try
            {
                var users = await _userServicesHttp.UpdateUserAsync(id, userhttp);
                return Ok(users);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a user by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the user to delete.</param>
        /// <returns>Returns information about the deleted user.</returns>
        /// <response code="200">Successful deletion. Returns information about the deleted user.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpDelete("{id}", Name = "deleteUser")]
        [ProducesResponseType(typeof(List<UserHttp>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the user is not found
        [ProducesResponseType(500)] // Specifies internal server error
        public async Task<ActionResult<List<UserHttp>>> DeleteUser(int id)
        {
            try
            {
                var users = await _userServicesHttp.DeleteUserAsync(id);
                return Ok(users);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
    
}

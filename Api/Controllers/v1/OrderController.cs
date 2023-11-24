using Api.Services.OrderServices;
using Api.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>Returns a list of all orders.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Order>), 200)] // Specifies the data type for a successful response
        public ActionResult<List<Order>> GetAllOrder()
        {
            var orders = _orderServices.GetAllOrder();
            return Ok(orders);
        }
        /// <summary>
        /// Get information about a specific order by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order.</param>
        /// <returns>Returns information about a specific order.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Order>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the order is not found
        public ActionResult<List<Order>> GetSingleOrder(int id)
        {
            var singleOrder = _orderServices.GetSingleOrder(id);
            if (singleOrder == null)
                return NotFound("Order not found.");
            return Ok(singleOrder);
        }
        /// <summary>
        /// Add a new order.
        /// </summary>
        /// <param name="order">Data for the new order.</param>
        /// <returns>Returns information about the added order.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<Order>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the order is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<List<Order>>> AddOrder(Order order)
        {
            try
            {
                var singleOrder = await _orderServices.AddOrderAsync(order);
                if (singleOrder == null)
                    return NotFound("Order not found.");
                return Ok(singleOrder);
            }
            catch (Exception)
            {
                return StatusCode(400, "Bed request");
            }
        }
        /// <summary>
        /// Update information about an order by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order to update.</param>
        /// <param name="order">New data for updating the order.</param>
        /// <returns>Returns information about the updated order.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<Order>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the order is not found
        [ProducesResponseType(400)] // Specifies the response for a bad request
        public async Task<ActionResult<List<Order>>> UpdateOrder(int id, Order order)
        {
            try
            {
                var singleOrder = await _orderServices.UpdateOrderAsync(id, order);
                if (singleOrder == null)
                    return NotFound("Order not found.");

                return Ok(singleOrder);
            }
            catch (Exception)
            {
                return StatusCode(400, "Bed request");
            }

        }
        /// <summary>
        /// Delete an order by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the order to delete.</param>
        /// <returns>Returns information about the deleted order.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<Order>), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(404)] // Specifies the response when the order is not found
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrderAsync(id);
            if (result == null)
                return NotFound("Order not found.");
            
            return Ok(result);
        }
    }
}


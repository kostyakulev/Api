using Api.Services.OrderServices;
using Api.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrder()
        {
            return await _orderServices.GetAllOrder();
        }
        [HttpGet("{id}")]
        public ActionResult<List<Order>> GetSingleOrder(int id)
        {
            var singleOrder = _orderServices.GetSingleOrder(id);
            if (singleOrder == null)
                return NotFound("Order not found.");
            return Ok(singleOrder);
        }
        [HttpPost]
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
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrderAsync(id);
            if (result == null)
                return NotFound("Order not found.");
            
            return Ok(result);
        }
    }
}


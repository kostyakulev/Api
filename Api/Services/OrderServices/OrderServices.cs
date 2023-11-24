using Api.ViewModels;

namespace Api.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly TestDatabaseContext _context;

        public OrderServices(TestDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> AddOrderAsync(Order order)
        {
            var newOrder = new Order
            {
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(x => new OrderDetail
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>?> DeleteOrderAsync(int id)
        {
            var singleOrder = _context.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Product).FirstOrDefault(p => p.OrderId == id);
            if (singleOrder == null)
                return null;
            _context.OrderDetails.RemoveRange(singleOrder.OrderDetails);
            _context.Orders.Remove(singleOrder);
            await _context.SaveChangesAsync();
            return await _context.Orders.ToListAsync();
        }

        public List<OrderDto> GetAllOrder() 
        {
            //var orders = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToListAsync();
            //return orders;
            var orders = _context.Orders
           .Include(o => o.User)
           .Include(o => o.OrderDetails)
               .ThenInclude(od => od.Product)
           .ToList();

            var orderModels = orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                UserName = order.User?.Username,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductName = od.Product?.ProductName,
                    Quantity = od.Quantity
                }).ToList()
            }).ToList();

            return orderModels;
        }

        public OrderDto GetSingleOrder(int id)
        {
            //var singleOrder = _context.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Product).FirstOrDefault(p => p.OrderId == id);
            //if (singleOrder == null)
            //    return null;
            //return singleOrder;
            var order = _context.Orders
            .Where(o => o.OrderId == id)
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
            .FirstOrDefault();

            if (order == null)
            {
                return null;
            }

            var ordermodels = new OrderDto
            {
                OrderId = order.OrderId,
                UserName = order.User.Username,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductName = od.Product?.ProductName,
                    Quantity = od.Quantity
                }).ToList()
            };
            return ordermodels;
        }

        public async Task<List<Order>?> UpdateOrderAsync(int id, Order order)
        {
            var singleOrder = await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);
        
            if (singleOrder == null)
                return null;
            singleOrder.UserId = order.UserId;
            _context.OrderDetails.RemoveRange(singleOrder.OrderDetails);
            singleOrder.OrderDetails = order.OrderDetails.Select( od => new OrderDetail
            {  
                ProductId = od.ProductId,
                Quantity = od.Quantity
            }).ToList();
            

            await _context.SaveChangesAsync();

            return await _context.Orders.ToListAsync();
        }
    }
}


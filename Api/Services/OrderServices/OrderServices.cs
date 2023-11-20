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
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>?> DeleteOrderAsync(int id)
        {
            var singleOrder = await _context.Orders.FindAsync(id);
            if (singleOrder == null)
                return null;

            _context.Orders.Remove(singleOrder);
            await _context.SaveChangesAsync();
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetAllOrder()
        {
            var orders = await _context.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Product).ToListAsync();
           return orders;
            
           
           
        }

        public async Task<Order?> GetSingleOrder(int id)
        {
            var singleOrder = _context.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Product).FirstOrDefault(p => p.OrderId == id);
            if (singleOrder == null)
                return null;
            return singleOrder;
        }

        public async Task<List<Order>?> UpdateOrderAsync(int id, Order order)
        {
            var singleOrder = await _context.Orders.FindAsync(id);
            if (singleOrder == null)
                return null;

            singleOrder.OrderId = order.OrderId;
            singleOrder.UserId = order.UserId;
            singleOrder.User = order.User;

            await _context.SaveChangesAsync();

            return await _context.Orders.ToListAsync();
        }
    }
}


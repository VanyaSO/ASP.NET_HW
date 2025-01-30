using Microsoft.AspNetCore.Mvc;
using hw_20_dop_1.Interfaces;

namespace hw_20_dop_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orders;
        private readonly ICart _carts;
        private readonly IUser _users;

        public OrderController(IOrder orders, ICart carts, IUser users)
        {
            _orders = orders;
            _carts = carts;
            _users = users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersAsync(string id)
        {
            var orders = await _orders.GetOrdersByUserIdAsync(id);
            var ordersDTO = orders.Select(o => OrderDTO(o)).ToList();

            return ordersDTO;
        }

        [HttpPost("add/{userId}")]
        public async Task<ActionResult<OrderDTO>> CreateOrder(string userId, OrderData orderData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _users.GetUserWithCartByIdAsync(userId);
            if (user == null) return BadRequest(new { message = "User not found" });
            
            var cart = await _carts.GetCartByIdAsync(user.CartId.ToString());
            if (cart == null || cart.CartLines.Count == 0) return BadRequest(new { message = "Products not found" });

            var newOrder = new Order()
            {
                UserId = user.Id,
                OrderData = new OrderData()
                {
                    CustomerName = orderData.CustomerName,
                    Address = orderData.Address,
                    City = orderData.City
                },
                Shipped = false,
                OrderLines = cart.CartLines.Select(cl => new OrderLine()
                {
                    BookId = cl.BookId,
                    Quantity = cl.Quantity
                }).ToList()
            };

            cart.LastUpdate = DateTime.Now;
            cart.CartLines.Clear();
            await _carts.UpdateCartAsync(cart);
            
            await _orders.AddOrderAsync(newOrder);
            return OrderDTO(newOrder);
        }

        private static OrderDTO OrderDTO(Order order)
        {
            var orderLinesDTO = order.OrderLines.Select(ol => OrderLineDTO(ol)).ToList();
            return new OrderDTO()
            {
                Id = order.Id,
                OrderData = order.OrderData,
                Shipped = order.Shipped,
                OrderLines = orderLinesDTO,
                TotalSum = orderLinesDTO.Sum(ord => ord.TotalSum)
            };
        }

        private static OrderLineDTO OrderLineDTO(OrderLine orderLine) => new OrderLineDTO()
        {
            Quantity = orderLine.Quantity,
            Book = new OrderBookDTO()
            {
                Id = orderLine.Book.Id,
                Title = orderLine.Book.Title,
                Price = orderLine.Book.Price
            },
            TotalSum = orderLine.Quantity * orderLine.Book.Price
        };
    }
}
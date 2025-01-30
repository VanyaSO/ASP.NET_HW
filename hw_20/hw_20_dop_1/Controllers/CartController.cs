using Microsoft.AspNetCore.Mvc;
using hw_20_dop_1.Interfaces;

namespace hw_20_dop_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart _carts;
        private readonly IBook _books;

        public CartController(ICart carts, IBook books)
        {
            _carts = carts;
            _books = books;
        }

        [HttpPost("add-book-to-cart")]
        public async Task<ActionResult<CartDTO>> AddBookToCartAsync(string cartId, string bookId, int quantity)
        {
            var cart = await _carts.GetCartByIdAsync(cartId);
            if (cart == null) return BadRequest(new { message = "Cart not found" });

            var bookExists = await _books.BookExistsAsync(bookId);
            if (!bookExists) return BadRequest(new { message = "Book not found" });

            if (quantity <= 0) return BadRequest("Quantity not be 0");

            
            var cl = cart.CartLines.FirstOrDefault(cl => cl.BookId.Equals(bookId));
            if (cl != null)
            {
                cl.Quantity += quantity;
            }
            else
            {
                cart.CartLines.Add(new CartLine()
                {
                    CartId = Guid.Parse(cartId),
                    Quantity = quantity,
                    BookId = Guid.Parse(bookId)
                });
            }

            await _carts.UpdateCartAsync(cart);
            return CartDTO(cart);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCartAsync(string id)
        {
            var cart = await _carts.GetCartByIdAsync(id);
            if (cart == null) return NotFound(new { message = "Cart not found" });

            return CartDTO(cart);
        }

        private static CartDTO CartDTO(Cart cart)
        {
            var cartLineDtos = cart.CartLines.Select(cl => CartLineDTO(cl)).ToList();
            
            return new CartDTO()
            {
                Id = cart.Id,
                CartLines = cartLineDtos,
                TotalSum = cartLineDtos.Sum(cld => cld.TotalSum)
            };
        }
        
        private static CartLineDTO CartLineDTO(CartLine cartLine) => new CartLineDTO()
        {
            Id = cartLine.CartId,
            Quantity = cartLine.Quantity,
            BookId = cartLine.BookId,
            BookTitle = cartLine.Book.Title,
            BookPrice = cartLine.Book.Price,
            TotalSum = cartLine.Quantity * cartLine.Book.Price
        };
        
    }
}

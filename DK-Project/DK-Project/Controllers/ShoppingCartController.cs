using BookStore.BL.Interfaces;
using DK_Project.DL.Interfaces;
using DK_Project.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DK_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IPurchaseRepository _purchaseRepository;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IPurchaseRepository purchaseRepository)
        {
            _shoppingCartService = shoppingCartService;
            _purchaseRepository = purchaseRepository;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int userId)
        {
            return Ok(await _shoppingCartService.GetContent(userId));
        }

        [HttpPost("EmptyCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmptyCart(int userId)
        {
            await _shoppingCartService.EmptyCart(userId);
            return Ok();
        }
        [HttpPost("AddPurchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPurchase(int userId, int bookId)
        {
            return Ok(await _shoppingCartService.AddToCart(userId, bookId));
        }
        [HttpDelete("RemoveFromCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromCart(int userId, int bookId)
        {
            return Ok(await _shoppingCartService.RemoveFromCart(userId, bookId));
        }
        [HttpPost("FinishPurchase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FinishPurchase(int userId)
        {
            await _shoppingCartService.FinishPurchase(userId);
            return Ok();
        }

        [HttpGet("GetFromMango")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCollection(int userId)
        {
            return Ok(await _purchaseRepository.GetPurchases(userId));
        }
    }
}

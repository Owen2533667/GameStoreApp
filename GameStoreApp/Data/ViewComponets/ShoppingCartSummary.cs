using GameStoreApp.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApp.Data.ViewComponets
{
    public class ShoppingCartSummary : ViewComponent
    {

        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke() 
        { 
            var items = _shoppingCart.GetCartItems();
            return View(items.Count);
        }
    }
}

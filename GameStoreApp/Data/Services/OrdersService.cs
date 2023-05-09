﻿using GameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly GameStoreAppDbContext _context;

        public OrdersService(GameStoreAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems)!.ThenInclude(x => x.Game).Include(x => x.User).ToListAsync();

            if(userRole != "admin")
            {
                orders = orders.Where(x => x.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    GameId = item.Game!.Id,
                    OrderId = order.Id,
                    Price = item.Game.Price
                };

                await _context.OrderItems.AddAsync(orderItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}

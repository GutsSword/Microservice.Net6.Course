using FreeCourse.Services.OrderService.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.OrderService.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }

        private readonly List<OrderItem> _orderItems;  // Backing Field

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;  // Kapsülleme - Sadece okuma olarak açılır.

        public Order()
        {
            
        }
        public Order(string buyerId, Address address)
        {
            BuyerId = buyerId;
            Address = address;
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
        }

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (existProduct is false)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);






    }

    

}

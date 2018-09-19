using System;
namespace SmartChef_API.Models
{
    public class NewOrder
    {
        public string OrderId { get; set; }
        public string State { get; set; }
        public SmartChef_API.Models.Product Product { get; set; }
        public string UserId { get; set; }
        public string ShopId { get; set; }
        public int QueuedOrderNumber { get; set; }
        public SmartChef_API.Models.Ingredients[] Ingredients { get; set; }
        public DateTime OrderQueuedDateTime { get; set; }
        public DateTime OrderInProgressDateTime { get; set; }
        public DateTime OrderCompletedDateTime { get; set;  }
    }
}

using System;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;

namespace Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : ControllerBase
    {
        // ADD THIS PART TO YOUR CODE
        string cosmosDBEndPoint = ConfigurationManager.AppSettings["COSMOSDB_URL"];
        string cosmosDBPrimaryKey = ConfigurationManager.AppSettings["COSMOSDBPRIMARYKEY"];
        private DocumentClient client;


        // GET api/order
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/order/submit
        [HttpPost("submit")]
        public void NewOrder(SmartChef_API.Models.NewOrder newOrder){

            client = new DocumentClient(new Uri(cosmosDBEndPoint), cosmosDBPrimaryKey);


            SmartChef_API.Models.NewOrder nOrder = new SmartChef_API.Models.NewOrder
            {
                OrderId = newOrder.OrderId,
                State = newOrder.State,
                Product = newOrder.Product, //new Product { ProductId = "1", ProductName = "Omlette" },
                UserId = newOrder.UserId,
                ShopId = newOrder.ShopId,
                QueuedOrderNumber = newOrder.QueuedOrderNumber,
                Ingredients = newOrder.Ingredients, //new SmartChef_API.Models.Ingredients[] { new SmartChef_API.Models.Ingredients { Name = "1", AllergicType = "Egg", Status = "Yes" } },
                OrderQueuedDateTime = DateTime.Now,
                OrderInProgressDateTime = DateTime.Now,
                OrderCompletedDateTime = DateTime.Now
            };
            client.ReadDocumentAsync(UriFactory.CreateDocumentUri("SmartChefDB", "ActiveOrders","2"));
            client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("SmartChefDB", "ActiveOrders"), nOrder);

        }

        // POST api/order
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

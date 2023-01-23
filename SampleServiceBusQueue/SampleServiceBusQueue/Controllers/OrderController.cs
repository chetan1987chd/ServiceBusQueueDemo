using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleServiceBusQueue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleServiceBusQueue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        string _connectionString = "<replace your connection string>";
        string _queueName = "orderqueue";


        public OrderController()
        {
        }

        [HttpGet]
        [Route("CreateOrder")]
        public async Task CreateOrder()
        {
           //creates the client
            ServiceBusClient queueClient = new ServiceBusClient(_connectionString);

            //prepares the sender
            ServiceBusSender queueSender = queueClient.CreateSender(_queueName);

            OrderModel orderDetails = new OrderModel()
            {
                OrderId = 12,
                Price = 4500,
                Quantity = 2,
                ProductId = 7
            };

            //stringify the object to json
            string jsonString = JsonSerializer.Serialize(orderDetails);

            //creates the message
            ServiceBusMessage message = new ServiceBusMessage(jsonString);

            //sends the message to queue
            await queueSender.SendMessageAsync(message);




        }
    }
}

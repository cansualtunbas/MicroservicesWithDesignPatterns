using MassTransit;
using Shared;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEvent> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(ILogger<StockReservedEvent> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var balance = 3000m; //decimal

            if (balance > context.Message.Payment.totalPrice)
            {
                _logger.LogInformation($"{context.Message.Payment.totalPrice} TL was withdrawn from credit card for user id={context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentCompletedEvent { BuyerId=context.Message.BuyerId, OrderId=context.Message.OrderId });
            }
            else
            {
                _logger.LogInformation($"{context.Message.Payment.totalPrice} TL was not withdrawn for credit card user id={context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentFailedEvent { BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId,Message="not enough balance",orderItems=context.Message.OrderItems });
            }

        }
    }
}

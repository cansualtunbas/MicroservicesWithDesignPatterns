using EventSourcing.API.EventStores;
using EventSourcing.API.Models;
using EventSourcingShared.Events;
using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;

namespace EventSourcing.API.BackgroundServices
{
    public class CategoryReadModelEventStore : BackgroundService
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly ILogger<CategoryReadModelEventStore> _logger;

        //background servise singleton ve dbcontext scoped. singleton'dan scope ulaşılamaz. scope'dan singleton2a erişilebilir. Aşağıdaki servis provider yapılmasında ki sebep bu

        private readonly IServiceProvider _serviceProvider;

        public CategoryReadModelEventStore(IEventStoreConnection eventStoreConnection, ILogger<CategoryReadModelEventStore> logger, IServiceProvider serviceProvider)
        {
            _eventStoreConnection = eventStoreConnection;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //autoAck:true olduğunda mesajın doğru işlenip işlenmediğine bakmaz(hata fırlatmazsa bir daha o eventi göndermez)
            //autoAck:false olduğunda ise mesajın doğru işlenip işlenmediğine ben sana haber vereceğim. arg1.Acknowledge(args2.Event.EventId);

            //true: EventAppeared exception fırlamadı ise event gönderildi sayar
            //false: EventAppeared arg1.Acknowledge(args2.Event.EventId) gönderilirse gönderildi say.
            await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(CategoryStream.StreamName, CategoryStream.GroupName, EventAppeared,autoAck:false);


            throw new NotImplementedException();
        }


        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent args2)
        {

            _logger.LogInformation("The message processing....");

            //syrı bir classlibrary olduğu için (Shared)
            var type = Type.GetType($"{Encoding.UTF8.GetString(args2.Event.Metadata)},EventSourcing.Shared");

            var eventData = Encoding.UTF8.GetString(args2.Event.Data);

            var @event = JsonSerializer.Deserialize(eventData, type);

            //işlem bittiğinde memoryden düştüğü için scope oluşturuldu.
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Category category = null;
            switch (@event)
            {
                case CategoryCreatedEvent categoryCreatedEvent:
                    category = new Category()
                    {
                        Name = categoryCreatedEvent.Name,
                        Id = categoryCreatedEvent.Id,
                        Price = categoryCreatedEvent.Price,
                        Stock = categoryCreatedEvent.Stock,
                        UserId = categoryCreatedEvent.UserId,
                    };
                    context.Category.Add(category);
                    break;
                case CategoryNameChangedEvent categoryNameChangedEvent:
                    category = context.Category.Find(categoryNameChangedEvent.Id);
                    if (category != null)
                    {
                        category.Name = categoryNameChangedEvent.ChangedName;
                    }
                    break;
                case CategoryPriceChangedEvent categoryPriceChangedEvent:
                    category = context.Category.Find(categoryPriceChangedEvent.Id);
                    if (category != null)
                    {
                        category.Price = categoryPriceChangedEvent.ChangedPrice;
                    }
                    break;

                case CategoryDeletedEvent categoryDeletedEvent:
                    category = context.Category.Find(categoryDeletedEvent.Id);
                    if (category != null)
                    {
                        context.Category.Remove(category);

                    }
                    break;
                default:
                    break;
            }

            await context.SaveChangesAsync();
            arg1.Acknowledge(args2.Event.EventId);
        }
    }
}


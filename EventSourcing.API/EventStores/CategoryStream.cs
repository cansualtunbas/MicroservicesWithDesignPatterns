using EventSourcing.API.DTOs;
using EventSourcingShared.Events;
using EventStore.ClientAPI;

namespace EventSourcing.API.EventStores
{
    public class CategoryStream : AbstractStream
    {
        public static string StreamName => "CategoryStream";
        public static string GroupName => "agroup";
        public CategoryStream(IEventStoreConnection eventStoreConnection) : base(eventStoreConnection, StreamName)
        {
        }

        public void Created(CreateCategoryDto createCategoryDto)
        {
            //linkedList kullanıldığı için addlast methodu geliyor
            Events.AddLast(new CategoryCreatedEvent { Id = Guid.NewGuid(), Name = createCategoryDto.Name, Price = createCategoryDto.Price, Stock = createCategoryDto.Stock, UserId = createCategoryDto.UserId });

        }

        public void NameChanged(ChangeCategoryNameDto changeCategoryNameDto)
        {

            Events.AddLast(new CategoryNameChangedEvent { Id = changeCategoryNameDto.Id, ChangedName = changeCategoryNameDto.Name });
        }

        public void PriceChanged(ChangeCategoryPriceDto changeCategoryPriceDto)
        {
            Events.AddLast(new CategoryPriceChangedEvent() { ChangedPrice = changeCategoryPriceDto.Price, Id = changeCategoryPriceDto.Id });
        }

        public void Deleted(Guid id)
        {
            Events.AddLast(new CategoryDeletedEvent { Id = id });
        }
    }
}

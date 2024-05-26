using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class ChangeCategoryPriceCommandHandler : IRequestHandler<ChangeCategoryPriceCommand>
    {
        private readonly CategoryStream _categoryStream;

        public ChangeCategoryPriceCommandHandler(CategoryStream categoryStream)
        {
            _categoryStream = categoryStream;
        }

        public async Task<Unit> Handle(ChangeCategoryPriceCommand request, CancellationToken cancellationToken)
        {
            _categoryStream.PriceChanged(request.ChangeCategoryPriceDto);
            await _categoryStream.SaveAsync();
            //mediatr birşey dönemyeceksen unit.value
            return Unit.Value;
        }
    }
}

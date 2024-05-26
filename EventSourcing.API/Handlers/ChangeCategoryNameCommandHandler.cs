using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class ChangeCategoryNameCommandHandler : IRequestHandler<ChangeCategoryNameCommand>
    {
        private readonly CategoryStream _categoryStream;

        public ChangeCategoryNameCommandHandler(CategoryStream categoryStream)
        {
            _categoryStream = categoryStream;
        }

        public async Task<Unit> Handle(ChangeCategoryNameCommand request, CancellationToken cancellationToken)
        {
            _categoryStream.NameChanged(request.ChangeCategoryNameDto);
            await _categoryStream.SaveAsync();
            //mediatr birşey dönemyeceksen unit.value
            return Unit.Value;
        }
    }
}

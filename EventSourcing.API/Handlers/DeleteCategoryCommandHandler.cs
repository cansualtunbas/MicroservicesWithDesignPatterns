using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly CategoryStream _categoryStream;

        public DeleteCategoryCommandHandler(CategoryStream categoryStream)
        {
            _categoryStream = categoryStream;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            _categoryStream.Deleted(request.Id);
            await _categoryStream.SaveAsync();
            //mediatr birşey dönemyeceksen unit.value
            return Unit.Value;
        }
    }
}

using EventSourcing.API.Commands;
using EventSourcing.API.EventStores;
using MediatR;

namespace EventSourcing.API.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly CategoryStream _categoryStream;

        public CreateCategoryCommandHandler(CategoryStream categoryStream)
        {
            _categoryStream = categoryStream;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _categoryStream.Created(request.CreateCategoryDto);
            await _categoryStream.SaveAsync();
            //mediatr birşey dönemyeceksen unit.value
            return Unit.Value;
        }
    }
}

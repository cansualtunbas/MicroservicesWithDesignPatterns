using MediatR;

namespace EventSourcing.API.Commands
{
    public class DeleteCategoryCommand:IRequest
    {
        public Guid Id { get; set; }
    }
}

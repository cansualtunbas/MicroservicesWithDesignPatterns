using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Commands
{
    public class CreateCategoryCommand:IRequest
    {
        public CreateCategoryDto CreateCategoryDto { get; set; }
    }
}

using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Commands
{
    public class ChangeCategoryNameCommand:IRequest
    {
        public ChangeCategoryNameDto ChangeCategoryNameDto { get; set; }
    }
}

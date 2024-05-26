using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Commands
{
    public class ChangeCategoryPriceCommand:IRequest
    {
        public ChangeCategoryPriceDto ChangeCategoryPriceDto { get; set; }
    }
}

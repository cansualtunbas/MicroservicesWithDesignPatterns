using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Queries
{
    public class GetCategoryAllListByUserId:IRequest<List<CategoryDto>>
    {
        public int UserId { get; set; }
    }
}

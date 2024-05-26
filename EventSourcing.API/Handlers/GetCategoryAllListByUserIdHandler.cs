using EventSourcing.API.DTOs;
using EventSourcing.API.Models;
using EventSourcing.API.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.API.Handlers
{
    public class GetCategoryAllListByUserIdHandler:IRequestHandler<GetCategoryAllListByUserId,List<CategoryDto>>
    {
        private readonly AppDbContext _context;

        public GetCategoryAllListByUserIdHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoryAllListByUserId request, CancellationToken cancellationToken)
        {
            var categories = await _context.Category.Where(x => x.UserId == request.UserId).ToListAsync();
            return categories.Select(x => new CategoryDto { Id = x.Id, UserId = x.UserId, Name = x.Name, Price = x.Price, Stock = x.Stock }).ToList();
        }
    }
}

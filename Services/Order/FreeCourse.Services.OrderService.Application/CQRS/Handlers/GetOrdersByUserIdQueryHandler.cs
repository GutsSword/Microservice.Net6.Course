using AutoMapper.Internal.Mappers;
using FreeCourse.Services.OrderService.Application.AutoMapper;
using FreeCourse.Services.OrderService.Application.CQRS.Queries;
using FreeCourse.Services.OrderService.Application.Dtos;
using FreeCourse.Services.OrderService.Infrastructure.AppContext;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.OrderService.Application.CQRS.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            this.context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.BuyerId==request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(),200);
            }

            var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return Response<List<OrderDto>>.Success(orderDto,200);

        }
    }
}

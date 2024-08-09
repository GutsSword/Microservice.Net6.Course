using Dapper;
using FreeCourse.DiscountService.Entities;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.DiscountService.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration configuration;
        private IDbConnection connection;

        public DiscountService(IConfiguration configuration)
        {
            this.configuration = configuration;

            connection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await connection.ExecuteAsync("Delete from discount where discountid=@DiscountId",
                new
                {
                    DiscountId = id,
                });
            if (status > 0)
                return Response<NoContent>.Success(200);
            else
                return Response<NoContent>.Fail("Discount not found.",404);

        }

        public async Task<Response<List<Discount>>> GetAllDiscounts()
        {
            var discounts = await connection.QueryAsync<Discount>("select * from discount ");
            return Response<List<Discount>>.Success(discounts.ToList(),200);
        }

        public async Task<Response<Discount>> GetByCodeByUserId(string code, string userId)
        {
            var discount = await connection.QueryAsync<Discount>("select * from discount where userid=@UserId and code=@Code", new
            {
                UserId = userId,
                Code = code,
            });

            var hasDiscount = discount.FirstOrDefault();

            
            if (hasDiscount == null)
                return Response<Discount>.Fail("Discount not found.", 404);
            else
                return Response<Discount>.Success(hasDiscount, 200); 
        }

        public async Task<Response<Discount>> GetByIdDiscount(int id)
        {
            var discount = (await connection.QueryAsync<Discount>("select * from discount where discountid=@DiscountId",
               new
               {
                   DiscountId = id
               })).SingleOrDefault();

            if (discount == null)
                return Response<Discount>.Fail("Discount Code Not Found.", 404);

            return Response<Discount>.Success(discount, 200);

        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            var status = await connection.ExecuteAsync
                ("insert into discount (userid,rate,code) values (@UserId,@Rate,@Code)", discount);

            if (status > 0)
                return Response<NoContent>.Success(204);
            else
                return Response<NoContent>.Fail("Something went wrong while adding.", 500);
        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {
            var status = await connection.ExecuteAsync
                ("update discount set userid=@UserId, code=@Code, rate=@Rate where discountid=@DiscountId",
                new
                {
                    DiscountId = discount.DiscountId,
                    UserId = discount.UserId,
                    Code = discount.Code,
                    Rate = discount.Rate,
                });

            if (status > 0)
                return Response<NoContent>.Success(204);
            else
                return Response<NoContent>.Fail("Discount not found.", 404);
        }
    }
}

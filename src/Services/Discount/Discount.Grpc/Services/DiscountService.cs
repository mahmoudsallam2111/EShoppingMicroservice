using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountContext _context;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(DiscountContext context, ILogger<DiscountService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            var coupon = await _context
                .coupons
                .FirstOrDefaultAsync(p => p.ProductName == request.ProductName);
            if (coupon is null)
            {
                // create a dummy coupon for contuning adding basking to db
                coupon = new Coupon() { ProductName = "no discount", Description = "no disc", Amount = 0 };
            }

            _logger.LogInformation($"discount is retrived for product name {request.ProductName} , with amount {coupon.Amount}");

            return coupon.Adapt<CouponModel>();


        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            _context.coupons.Add(coupon);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon is created successfully");

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            var couponToUpdate = await _context.coupons.Where(c=>c.Id == coupon.Id).FirstOrDefaultAsync();
            if (couponToUpdate is null)
                throw new RpcException(new Status(StatusCode.NotFound, "NotFound request object."));

            // _context.coupons.Update(coupon);   // if i make this i have an error cause ef detaced coupon object and start tracking couponToUpdate object
            // so, we can not track to entity in the same time 

            couponToUpdate.Id = coupon.Id;
            couponToUpdate.ProductName = coupon.ProductName;
            couponToUpdate.Description = coupon.Description;
            couponToUpdate.Amount = coupon.Amount;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon is updated successfully");

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context
                .coupons
                .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, "NotFound request object."));

            _context.coupons.Remove(coupon);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon is Removed successfully");

            return new DeleteDiscountResponse { Success = true};
        }
    }
}

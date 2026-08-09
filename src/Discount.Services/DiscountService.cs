using Discount.Domain;

namespace Discount.Services;

public sealed class DiscountService : IDiscountService
{
    public DiscountResponse CalculateDiscount(DiscountRequest request)
    {
        var tierBaseDiscount = GetTierBaseDiscount(request.CustomerTier);
        var qualifyingHistory = request.PastOrders
            .Where(order => !order.IsCancelledOrRefunded)
            .Where(order => order.CompletedAt >= DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1)))
            .ToList();

        var historyModifier = qualifyingHistory.Count switch
        {
            >= 3 => 4m,
            2 => 4m,
            1 => 2m,
            _ => 0m
        };

        var discountPercentage = qualifyingHistory.Count > 0 ? tierBaseDiscount + historyModifier : 0m;
        var discountAmount = discountPercentage > 0 ? request.OrderSubtotal * discountPercentage / 100m : 0m;

        var reason = discountPercentage > 0
            ? $"{request.CustomerTier} tier discount plus {historyModifier} percent history modifier"
            : "No qualifying history for discount";

        return new DiscountResponse(
            request.OrderSubtotal,
            discountPercentage,
            discountAmount,
            reason);
    }

    private static decimal GetTierBaseDiscount(CustomerTier customerTier) => customerTier switch
    {
        CustomerTier.Bronze => 2m,
        CustomerTier.Silver => 5m,
        CustomerTier.Gold => 8m,
        _ => 0m
    };
}

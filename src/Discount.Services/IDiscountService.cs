using Discount.Domain;

namespace Discount.Services;

/// <summary>
/// Defines the service boundary for calculating customer discounts.
/// </summary>
public interface IDiscountService
{
    /// <summary>
    /// Calculates the discount for an order based on customer tier, subtotal, and past completed orders.
    /// </summary>
    /// <param name="request">The discount request containing customer and order details.</param>
    /// <returns>The discount response describing the applied discount.</returns>
    DiscountResponse CalculateDiscount(DiscountRequest request);
}

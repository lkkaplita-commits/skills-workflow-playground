namespace Discount.Domain;

/// <summary>
/// The result of calculating a discount for an order.
/// </summary>
/// <param name="DiscountableAmount">The amount eligible for a discount.</param>
/// <param name="DiscountPercentage">The applied discount percentage, expressed as a whole number (for example 10 for 10%).</param>
/// <param name="DiscountAmount">The monetary amount deducted from the discountable amount.</param>
/// <param name="DiscountReason">A human-readable explanation for why the discount was applied.</param>
public sealed record DiscountResponse(
    decimal DiscountableAmount,
    decimal DiscountPercentage,
    decimal DiscountAmount,
    string DiscountReason
);

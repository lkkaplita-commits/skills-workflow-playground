namespace Discount.Domain;

/// <summary>
/// The request payload for calculating a discount.
/// </summary>
/// <param name="CustomerTier">The customer's current tier.</param>
/// <param name="OrderSubtotal">The current order subtotal before tax, shipping, or discounts.</param>
/// <param name="PastOrders">The customer's completed past orders within the last 12 months.</param>
public sealed record DiscountRequest(
    CustomerTier CustomerTier,
    decimal OrderSubtotal,
    IReadOnlyList<PastOrder> PastOrders
);

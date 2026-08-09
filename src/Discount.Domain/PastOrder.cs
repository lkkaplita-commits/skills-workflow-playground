namespace Discount.Domain;

/// <summary>
/// A completed order placed by a customer within the last 12 months,
/// excluding cancelled or refunded orders.
/// </summary>
/// <param name="Subtotal">The price of the completed order before tax, shipping, or discounts.</param>
/// <param name="CompletedAt">The date when the order was completed.</param>
/// <param name="IsCancelledOrRefunded">Whether the order was cancelled or refunded and should be excluded from history.</param>
public sealed record PastOrder(decimal Subtotal, DateOnly CompletedAt, bool IsCancelledOrRefunded = false);

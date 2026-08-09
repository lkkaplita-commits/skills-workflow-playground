namespace Discount.Domain;

/// <summary>
/// A completed order placed by a customer within the last 12 months,
/// excluding cancelled or refunded orders.
/// </summary>
/// <param name="Subtotal">The price of the completed order before tax, shipping, or discounts.</param>
/// <param name="CompletedAt">The date when the order was completed.</param>
public sealed record PastOrder(decimal Subtotal, DateOnly CompletedAt);

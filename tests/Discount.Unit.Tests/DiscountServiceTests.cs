using Discount.Domain;
using Discount.Services;

namespace Discount.Unit.Tests;

public class DiscountServiceTests
{
    [Test]
    public void CalculateDiscount_applies_expected_discount_for_each_tier()
    {
        var cases = new[]
        {
            (Tier: CustomerTier.Bronze, ExpectedPercentage: 6m, ExpectedAmount: 12m),
            (Tier: CustomerTier.Silver, ExpectedPercentage: 9m, ExpectedAmount: 18m),
            (Tier: CustomerTier.Gold, ExpectedPercentage: 12m, ExpectedAmount: 24m)
        };

        foreach (var testCase in cases)
        {
            var service = new DiscountService();
            var request = new DiscountRequest(
                testCase.Tier,
                200m,
                new List<PastOrder>
                {
                    new PastOrder(120m, DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-2))),
                    new PastOrder(90m, DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-4)))
                });

            var response = service.CalculateDiscount(request);

            Assert.That(response.DiscountableAmount, Is.EqualTo(200m), testCase.Tier.ToString());
            Assert.That(response.DiscountPercentage, Is.EqualTo(testCase.ExpectedPercentage), testCase.Tier.ToString());
            Assert.That(response.DiscountAmount, Is.EqualTo(testCase.ExpectedAmount), testCase.Tier.ToString());
            Assert.That(response.DiscountReason, Does.Contain(testCase.Tier.ToString()), testCase.Tier.ToString());
            Assert.That(response.DiscountReason, Does.Contain("history"), testCase.Tier.ToString());
        }
    }

    [Test]
    public void CalculateDiscount_excludes_cancelled_or_refunded_orders_and_returns_zero_discount_when_history_is_not_qualifying()
    {
        var service = new DiscountService();
        var request = new DiscountRequest(
            CustomerTier.Gold,
            200m,
            new List<PastOrder>
            {
                new PastOrder(120m, DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-2)), IsCancelledOrRefunded: true),
                new PastOrder(90m, DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1).AddDays(-1)))
            });

        var response = service.CalculateDiscount(request);

        Assert.That(response.DiscountableAmount, Is.EqualTo(200m));
        Assert.That(response.DiscountPercentage, Is.EqualTo(0m));
        Assert.That(response.DiscountAmount, Is.EqualTo(0m));
        Assert.That(response.DiscountReason, Does.Contain("No qualifying"));
    }
}

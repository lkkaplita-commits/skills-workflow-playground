using Discount.Domain;
using Discount.Services;

namespace Discount.Unit.Tests;

public class DiscountContractTests
{
    [Test]
    public void DiscountRequest_and_DiscountResponse_can_be_constructed_from_domain_contract()
    {
        var pastOrders = new List<PastOrder>
        {
            new PastOrder(150m, DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-2)))
        };

        var request = new DiscountRequest(
            CustomerTier.Gold,
            200m,
            pastOrders);

        Assert.That(request.CustomerTier, Is.EqualTo(CustomerTier.Gold));
        Assert.That(request.OrderSubtotal, Is.EqualTo(200m));
        Assert.That(request.PastOrders, Is.SameAs(pastOrders));
    }

    [Test]
    public void DiscountService_contract_exposes_CalculateDiscount_method()
    {
        var serviceType = typeof(IDiscountService);

        Assert.That(serviceType.GetMethod("CalculateDiscount"), Is.Not.Null);
        Assert.That(serviceType.GetMethod("CalculateDiscount")!.ReturnType, Is.EqualTo(typeof(DiscountResponse)));
    }
}

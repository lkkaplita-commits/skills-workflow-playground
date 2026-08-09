using System.Net;
using System.Net.Http.Json;
using Discount.Domain;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Discount.Unit.Tests;

public class DiscountApiContractTests
{
    [Test]
    public async Task PostDiscount_returns_zero_discount_for_unknown_customer()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/discount", new
        {
            customerId = "unknown-customer",
            orderSubtotal = 200m
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var payload = await response.Content.ReadFromJsonAsync<DiscountResponse>();

        Assert.That(payload, Is.Not.Null);
        Assert.That(payload!.DiscountableAmount, Is.EqualTo(200m));
        Assert.That(payload.DiscountPercentage, Is.EqualTo(0m));
        Assert.That(payload.DiscountAmount, Is.EqualTo(0m));
        Assert.That(payload.DiscountReason, Does.Contain("No qualifying"));
    }

    [Test]
    public async Task PostDiscount_returns_discount_for_qualifying_customer()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/discount", new
        {
            customerId = "gold-customer",
            orderSubtotal = 200m
        });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var payload = await response.Content.ReadFromJsonAsync<DiscountResponse>();

        Assert.That(payload, Is.Not.Null);
        Assert.That(payload!.DiscountableAmount, Is.EqualTo(200m));
        Assert.That(payload.DiscountPercentage, Is.EqualTo(12m));
        Assert.That(payload.DiscountAmount, Is.EqualTo(24m));
        Assert.That(payload.DiscountReason, Does.Contain("history"));
    }
}

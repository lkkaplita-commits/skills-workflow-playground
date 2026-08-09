using Discount.Domain;
using Discount.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IDiscountService, DiscountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var customerProfiles = new Dictionary<string, CustomerProfile>(StringComparer.OrdinalIgnoreCase)
{
    ["gold-customer"] = new CustomerProfile(
        CustomerTier.Gold,
        new List<PastOrder>
        {
            new(100m, DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-2))),
            new(120m, DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-4)))
        })
};

app.MapPost("/discount", (DiscountRequestInput input, IDiscountService discountService) =>
{
    if (!customerProfiles.TryGetValue(input.CustomerId, out var customerProfile))
    {
        customerProfile = new CustomerProfile(CustomerTier.Bronze, Array.Empty<PastOrder>());
    }

    var request = new DiscountRequest(
        customerProfile.CustomerTier,
        input.OrderSubtotal,
        customerProfile.PastOrders);

    var response = discountService.CalculateDiscount(request);
    return Results.Ok(response);
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.Run();

public partial class Program;

internal sealed record DiscountRequestInput(string CustomerId, decimal OrderSubtotal);

internal sealed record CustomerProfile(CustomerTier CustomerTier, IReadOnlyList<PastOrder> PastOrders);

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

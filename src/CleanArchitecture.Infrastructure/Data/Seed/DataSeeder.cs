using System;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Infrastructure.Data.DataContext;

namespace CleanArchitecture.Infrastructure.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Products.Any())
            return;

        var products = new List<Product>
        {
            Product.Create("Laptop Pro 15", "High-performance laptop with 16GB RAM", 
                Money.Of(1299.99m), 50, "Electronics"),
            Product.Create("Wireless Mouse", "Ergonomic wireless mouse with USB receiver", 
                Money.Of(29.99m), 200, "Electronics"),
            Product.Create("Mechanical Keyboard", "RGB mechanical keyboard with blue switches", 
                Money.Of(89.99m), 100, "Electronics"),
            Product.Create("USB-C Hub", "7-in-1 USB-C hub with HDMI and USB 3.0", 
                Money.Of(49.99m), 150, "Accessories"),
            Product.Create("Laptop Backpack", "Water-resistant laptop backpack", 
                Money.Of(59.99m), 75, "Accessories"),
            Product.Create("Monitor 27\"", "4K UHD monitor with HDR support", 
                Money.Of(399.99m), 30, "Electronics"),
            Product.Create("Webcam HD", "1080p webcam with built-in microphone", 
                Money.Of(79.99m), 120, "Electronics"),
            Product.Create("Desk Lamp LED", "Adjustable LED desk lamp with USB charging", 
                Money.Of(34.99m), 90, "Office Supplies")
        };

        await context.Products.AddRangeAsync(products);

        var customers = new List<Customer>
        {
            Customer.Create(
                "John", "Doe",
                Email.Create("john.doe@example.com"),
                "+1234567890",
                Address.Create("123 Main St", "New York", "NY", "10001", "USA")
            ),
            Customer.Create(
                "Jane", "Smith",
                Email.Create("jane.smith@example.com"),
                "+1987654321",
                Address.Create("456 Oak Ave", "Los Angeles", "CA", "90001", "USA")
            ),
            Customer.Create(
                "Mike", "Johnson",
                Email.Create("mike.johnson@example.com"),
                "+1122334455",
                Address.Create("789 Pine Rd", "Chicago", "IL", "60601", "USA")
            )
        };

        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();
    }
}

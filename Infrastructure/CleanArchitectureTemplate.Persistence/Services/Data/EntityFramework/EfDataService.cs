﻿using CleanArchitectureTemplate.Application.Abstractions.Services;
using CleanArchitectureTemplate.Domain.Constants.Enums;
using CleanArchitectureTemplate.Domain.Constants.SmartEnums.Localizations;
using CleanArchitectureTemplate.Domain.Entities.Membership;
using CleanArchitectureTemplate.Domain.Entities.Ordering;
using CleanArchitectureTemplate.Domain.Entities.Shopping;
using CleanArchitectureTemplate.Domain.ValueObjects;
using CleanArchitectureTemplate.Persistence.Contexts;
using CleanArchitectureTemplate.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Persistence.Services.Data.EntityFramework;

/// <summary>
/// Represents the data service for seeding and updating the database based on Entity Framework.
/// </summary>
public class EfDataService : IDataService
{
    private readonly EfDbContext _context;
    public readonly UserManager<AppUser> _userManager;
    public readonly RoleManager<AppRole> _roleManager;

    public EfDataService(EfDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <inheritdoc/>
    public async Task SeedAsync()
    {
        if (_context.Roles.Any())
            return;

        #region Seed Roles

        var roles = new[] { "Admin", "Customer", "Store" };

        foreach (var roleName in roles)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new AppRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }
        }

        #endregion Seed Roles

        #region Seed Users

        var adminAppUser = new AppUser
        {
            FullName = "Admin User",
            UserName = "admin",
            Email = "admin@localhost.com",
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(adminAppUser, "Admin@123");
        await _userManager.AddToRoleAsync(adminAppUser, "Admin");

        var storeAppUser = new AppUser
        {
            FullName = "Store User",
            UserName = "store",
            Email = "store@localhost.com",
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(storeAppUser, "Store@123");
        await _userManager.AddToRoleAsync(storeAppUser, "Store");

        var customerAppUser = new AppUser
        {
            FullName = "Customer User",
            UserName = "customer",
            Email = "customer@localhost.com",
            EmailConfirmed = false
        };

        await _userManager.CreateAsync(customerAppUser, "Customer@123");
        await _userManager.AddToRoleAsync(customerAppUser, "Customer");

        #endregion Seed Users

        #region Seed Customers

        var customerMember = new Customer
        {
            UserId = customerAppUser.Id,
            Age = 25,
            Gender = Gender.Male,
        };

        _context.Customers.Add(customerMember);

        #endregion Seed Customers

        #region Seed Stores

        var storeMember = new Store
        {
            UserId = storeAppUser.Id,
            Website = "www.store.com",
            Description = "Store Description",
        };
        _context.Stores.Add(storeMember);

        #endregion Seed Stores

        #region Seed Categories

        var electronicsCategory = new Category { Name = "Electronics", Description = "Electronic devices and accessories" };
        var clothingCategory = new Category { Name = "Clothing", Description = "Clothing and accessories" };
        var booksCategory = new Category { Name = "Books", Description = "Books and magazines" };

        _context.Categories.AddRange(electronicsCategory, clothingCategory, booksCategory);

        #endregion Seed Categories

        #region Seed Products

        var laptopProduct = new Product
        {
            Name = "Laptop",
            Description = "A high-performance laptop",
            Categories = [electronicsCategory],
            Stock = 100,
            StandardPrice = new Money(1000, Currency.Usd),
            DiscountRate = 0.1m,
            StoreId = storeMember.Id,
        };

        var phoneProduct = new Product
        {
            Name = "Phone",
            Description = "Latest model phone",
            Categories = [electronicsCategory],
            Stock = 200,
            StandardPrice = new Money(0.01m, Currency.Btc),
            DiscountRate = 0.2m,
            StoreId = storeMember.Id,
        };

        var shirtProduct = new Product
        {
            Name = "Shirt",
            Description = "A casual shirt",
            Categories = [clothingCategory],
            Stock = 1000,
            StandardPrice = new Money(50, Currency.Try),
            DiscountRate = 0.1m,
            StoreId = storeMember.Id,
        };

        var bookProduct = new Product
        {
            Name = "Book",
            Description = "A best-selling book",
            Categories = [booksCategory],
            Stock = 2000,
            StandardPrice = new Money(20, Currency.Eur),
            DiscountRate = 0.5m,
            StoreId = storeMember.Id,
        };

        _context.Products.AddRange(laptopProduct, phoneProduct, shirtProduct, bookProduct);

        #endregion Seed Products

        #region Seed Baskets

        var basket1 = new Basket { CustomerId = customerMember.Id };

        var basket2 = new Basket { CustomerId = customerMember.Id };

        var basket3 = new Basket { CustomerId = customerMember.Id };

        var basket4 = new Basket { CustomerId = customerMember.Id };

        _context.Baskets.AddRange(basket1, basket2, basket3, basket4);

        #endregion Seed Baskets

        #region Seed Basket Items

        var laptopBasketItem = new BasketItem
        {
            Product = laptopProduct,
            Quantity = 2,
            Basket = basket1,
        };

        var phoneBasketItem = new BasketItem
        {
            Product = phoneProduct,
            Quantity = 1,
            Basket = basket1,
        };

        var shirtBasketItem = new BasketItem
        {
            Product = shirtProduct,
            Quantity = 3,
            Basket = basket2,
        };

        var bookBasketItem1 = new BasketItem
        {
            Product = bookProduct,
            Quantity = 4,
            Basket = basket3,
        };

        var bookBasketItem2 = new BasketItem
        {
            Product = bookProduct,
            Quantity = 2,
            Basket = basket4,
        };

        var bookBasketItem3 = new BasketItem
        {
            Product = bookProduct,
            Quantity = 10,
            Basket = basket1,
        };

        _context.BasketItems.AddRange(laptopBasketItem, phoneBasketItem, shirtBasketItem, bookBasketItem1, bookBasketItem2, bookBasketItem3);

        #endregion Seed Basket Items

        #region Seed Orders

        var shippedOrder = new Order
        {
            Basket = basket1,
            ShippingAddress = new Address("12345", "Istanbul", Country.Turkey),
            Status = OrderStatus.Shipped,
        };

        var deliveredOrder = new Order
        {
            Basket = basket2,
            ShippingAddress = new Address("54321", "Izmir", Country.Turkey),
            Status = OrderStatus.Delivered,
        };

        var pendingOrder = new Order
        {
            Basket = basket3,
            ShippingAddress = new Address("67890", "Ankara", Country.Turkey),
            Status = OrderStatus.Pending,
        };

        _context.Orders.AddRange(shippedOrder, deliveredOrder, pendingOrder);

        #endregion Seed Orders

        #region Seed Invoices

        var shippedInvoice = new Invoice
        {
            Order = shippedOrder,
            BillingAddress = new Address("12345", "Istanbul", Country.Turkey),
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = InvoiceStatus.Paid,
            PaymentMethod = PaymentMethod.CreditCard,
            TransactionId = "0001",
            Notes = "Paid by credit card",
            PaidAt = DateTime.UtcNow.AddDays(-1),
        };

        var deliveredInvoice = new Invoice
        {
            Order = deliveredOrder,
            BillingAddress = new Address("54321", "Izmir", Country.Turkey),
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = InvoiceStatus.Paid,
            PaymentMethod = PaymentMethod.CreditCard,
            TransactionId = "0002",
            Notes = "Paid by credit card",
            PaidAt = DateTime.UtcNow.AddDays(-1),
        };

        var pendingInvoice = new Invoice
        {
            Order = pendingOrder,
            BillingAddress = new Address("67890", "Ankara", Country.Turkey),
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = InvoiceStatus.Pending,
        };

        _context.Invoices.AddRange(shippedInvoice, deliveredInvoice, pendingInvoice);

        #endregion Seed Invoices

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task MigrateAsync()
    {
        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
            await _context.Database.MigrateAsync();
    }
}
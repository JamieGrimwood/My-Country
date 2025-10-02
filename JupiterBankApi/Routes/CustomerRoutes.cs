using JupiterBankApi.Data;
using JupiterBankApi.Models;
using JupiterBank.Shared.Models;
using JupiterBank.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace JupiterBankApi.Routes;

public static class CustomerRoutes
{
    public static IEndpointRouteBuilder MapCustomerRoutes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/customers");

        group.MapPost("/create", async (AppDbContext db, Customer customer) =>
        {
            // First, we create the customer
            db.Customers.Add(customer);
            db.SaveChanges();

            // Then we create the bank account associated with the customer 
            var account = new BankAccount
            {
                CustomerId = customer.Id,
                AccountNumber = new Random().Next(10000000, 99999999).ToString(),
                SortCode = "12-34-56", // hardcoded for now
                Balance = 0,
                AccountType = AccountType.Current,
                OverdraftLimit = 0
            };

            db.BankAccounts.Add(account);
            db.SaveChanges();

            // Now, we add the card assosiated with the account
            var card = new Card
            {
                BankAccountId = account.Id,
                CardNumber = "4567" + new Random().Next(1000, 9999).ToString() +  new Random().Next(1000, 9999).ToString() +  new Random().Next(1000, 9999).ToString(),
                ExpiryMonth = 12,
                ExpiryYear = DateTime.UtcNow.Year + 3,
                CVV = new Random().Next(100, 999).ToString(),
                CardType = CardType.Debit,
                IsActive = true
            };

            db.Cards.Add(card);
            db.SaveChanges();

            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            var bankAccountDto = new BankAccountDto
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,
                SortCode = account.SortCode,
                Balance = account.Balance,
                AccountType = account.AccountType,
                OverdraftLimit = account.OverdraftLimit
            };

            var cardDto = new CardDto
            {
                Id = card.Id,
                CardNumber = card.CardNumber,
                ExpiryMonth = card.ExpiryMonth,
                ExpiryYear = card.ExpiryYear,
                CardType = card.CardType,
                IsActive = card.IsActive
            };

            var response = new
            {
                customer = customerDto,
                bankAccount = bankAccountDto,
                card = cardDto
            };

            return Results.Ok(ApiResponse<object>.Ok(response));
        });

        return routes;
    }
}
using GameRankPaymentSystem.Interfaces;
using GameRankPaymentSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GameRankPaymentSystem.Services;

public class OneCService : IOneCService
{
    public async Task AddTransaction(string CardNumber, string TransactionDate , decimal Amount , string Contact)
    {
        
    }
}
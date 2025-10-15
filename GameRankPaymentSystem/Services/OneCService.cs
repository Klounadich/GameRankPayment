using GameRankPaymentSystem.Interfaces;
using GameRankPaymentSystem.Models;

namespace GameRankPaymentSystem.Services;

public class OneCService : IOneCService
{
    public async Task AddTransaction(string CardNumber, string TransactionDate , float Amount , string Contact)
    {
        // подключение и сохранение в 1С
    }
}
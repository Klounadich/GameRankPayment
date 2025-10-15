using GameRankPaymentSystem.Models;
namespace GameRankPaymentSystem.Interfaces;

public interface IOneCService
{
    public Task AddTransaction(string CardNumber, string TransactionDate , float Amount , string Contact);
    
}
using GameRankPaymentSystem.Models;
namespace GameRankPaymentSystem.Interfaces;

public interface IPaymentService
{
    public  Task<bool> Pay(CardData cardData , float amount);
}
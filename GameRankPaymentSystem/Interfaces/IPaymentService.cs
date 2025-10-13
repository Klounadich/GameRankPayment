using GameRankPaymentSystem.Models;
namespace GameRankPaymentSystem.Interfaces;

public interface IPaymentService
{
    public  bool Pay(CardDataForPay cardData);
}
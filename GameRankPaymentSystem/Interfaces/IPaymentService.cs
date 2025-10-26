using GameRankPaymentSystem.Models;
using GameRankPaymentSystem.Models.DTOs;

namespace GameRankPaymentSystem.Interfaces;

public interface IPaymentService
{
    public  Task<bool> AsyncPay(CardDataForPay cardData ,  string payerContact);
}
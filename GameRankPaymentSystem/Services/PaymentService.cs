using GameRankPaymentSystem.Interfaces;
using GameRankPaymentSystem.Models;
using GameRankPaymentSystem.ValidatorModules;
namespace GameRankPaymentSystem.Services;

public class PaymentService : IPaymentService
{
    private readonly CardExpirationCheck _cardCheck;
    public PaymentService(CardExpirationCheck cardCheck)
    {
        _cardCheck=cardCheck;
    }
    public  bool Pay(CardDataForPay cardData)
    {
        try
        {
            // чекаем срок текущей карты
            var isFresh = _cardCheck.ValidateCardExpiration(cardData.CardExpiration);
            if (isFresh)
            {
                Console.WriteLine("Card expiration valid");
                // запрос на оплату
                var isPay = _cardCheck.Pay(cardData);
                if (isPay)
                {
                    // траим сохранить в 1с
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }
}
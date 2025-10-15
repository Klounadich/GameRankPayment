using GameRankPaymentSystem.Interfaces;
using GameRankPaymentSystem.Models;
using GameRankPaymentSystem.ValidatorModules;
namespace GameRankPaymentSystem.Services;

public class PaymentService : IPaymentService
{
    private readonly CardExpirationCheck _cardCheck;
    private readonly IOneCService _oneC;
    public PaymentService(CardExpirationCheck cardCheck , IOneCService oneC)
    {
        _oneC = oneC;
        _cardCheck=cardCheck;
    }
    public  bool Pay(CardDataForPay cardData , string PayerContact)
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
                      _oneC.AddTransaction(cardData.CardNumber , DateTime.Now.ToShortDateString(), cardData.Amount , PayerContact);
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
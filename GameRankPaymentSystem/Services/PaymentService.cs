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
    public async Task<bool> Pay(CardData cardData , float amount)
    {
        try
        {
            // чекаем срок текущей карты
            var isFresh = _cardCheck.ValidateCardExpiration(cardData.CardExpiration);
            if (isFresh)
            {
                
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
}
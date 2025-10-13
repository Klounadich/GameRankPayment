using GameRankPaymentSystem.Models;

namespace GameRankPaymentSystem.ValidatorModules;

public class CardExpirationCheck
{
    public bool ValidateCardExpiration(string cardExpiration)
    {
        var YearNow =DateTime.Now.Year %100;
        var MonthNow = DateTime.Now.Month;
        var monthExpiration = cardExpiration.Substring(0, 2);
        var YearExpiration = cardExpiration.Substring(2, 2);
        if (YearNow < int.Parse(YearExpiration))
        {
            return true;
        }

        if (YearNow == int.Parse(YearExpiration))
        {
            if (MonthNow < int.Parse(monthExpiration))
            {
                return true;
            }
        }
        return false;

    }

    public bool Pay(CardDataForPay cardData)
    {
        if (cardData.Amount > 0)
        {
            return true; // провели оплату
            
        }
        return false;
    }
}
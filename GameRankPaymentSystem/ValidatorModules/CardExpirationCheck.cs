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
        return true;

    }
}
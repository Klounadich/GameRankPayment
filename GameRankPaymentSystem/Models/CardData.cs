namespace GameRankPaymentSystem.Models;

public class CardData
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string CardExpiration { get; set; }
    public string CardSecurityNumber { get; set; }

}

public class CardDataForPay
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string CardExpiration { get; set; }
    public string CardSecurityNumber { get; set; }
    public float Amount { get; set; }

}
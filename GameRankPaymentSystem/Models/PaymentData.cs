using System.ComponentModel.DataAnnotations;
namespace GameRankPaymentSystem.Models;

public class PaymentData 
{
   [Key]
    public string userId {get; set;}
    public string cardNumber {get; set;}
    public string cardExpiration {get; set;}
    public string cardHolderName {get; set;}
    public string cardSecurityCode{ get; set;}
    public bool StopList { get; set; }
}
using Microsoft.AspNetCore.Mvc;
using GameRankPaymentSystem.Models;
using GameRankPaymentSystem.ValidatorModules;
using GameRankPaymentSystem.Data;
using System.Security.Claims;
using GameRankPaymentSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GameRankPaymentSystem.Controllers;
[ApiController]
[Route("api3/addpayment")]
public class AddPaymentController:ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly PaymentDBContext _context;

    public AddPaymentController(PaymentDBContext context , IPaymentService paymentService)
    {
        _context = context;
        _paymentService = paymentService;
    }

    [HttpPost("pay")]
    [Authorize]
    public IActionResult Donate([FromBody] CardDataForPay cardData)
    {
        string? payerContact = User.FindFirstValue(ClaimTypes.Email);
        var isPay =  _paymentService.Pay(cardData , payerContact);
        if (isPay)
        {
            return Ok(new { Message = "Оплата прошла успешно" });
        }
        return BadRequest();
    }
    [HttpPost("savepay")]
    [Authorize]
    public async Task<IActionResult> Donate([FromBody] string CardNumber)
    {
        var findsCard = _context.PaymentData.Where(x => x.cardNumber == CardNumber).ToList();
        if (findsCard.Count > 0)
        {
            var cardData = new CardDataForPay()
            {
                CardNumber = CardNumber,
                CardExpiration = findsCard.FirstOrDefault().cardExpiration,
                CardHolderName = findsCard.FirstOrDefault().cardHolderName,
                CardSecurityNumber = findsCard.FirstOrDefault().cardSecurityCode,
                Amount = 500 // затычка


            };
            
            string? payerContact = User.FindFirstValue(ClaimTypes.Email);
            var isPay =  _paymentService.Pay(cardData , payerContact);
            if (isPay)
            {
                return Ok(new { Message = "Оплата прошла успешно" });
            }
            
            
        }
        else
        {
            return BadRequest(new {Message="Не удалось найти данные карты"});
        }

        return Ok();
    }
    [HttpPost("addcard")]
    [Authorize]
    public async Task<IActionResult> AddCard([FromBody] CardData cardData)
    {
        try
        {
            var validator = new CardValidator();
            var validationResult = await validator.ValidateAsync(cardData);
            if (!validationResult.IsValid)
            {
                var firsterror = validationResult.Errors.First().ErrorMessage;
                return BadRequest(new { Message = firsterror });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CardInfo = new PaymentData()
            {
                cardNumber = cardData.CardNumber,
                cardExpiration = cardData.CardExpiration,
                cardHolderName = cardData.CardHolderName ,
                cardSecurityCode= cardData.CardSecurityNumber,
                StopList = false,
                userId = userId
            };
            _context.Add(CardInfo);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Карта успешно добавлена" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Messsage = "Произошла ошибка при добавлении карты"});
        }

    }

    [HttpPost("deletecard")]
    [Authorize]
    public async Task<IActionResult> DeleteCard([FromBody] string cardNumber)
    {
        try
        {
            var findsCard = _context.PaymentData.FirstOrDefault(x => x.cardNumber == cardNumber);
            if (findsCard != null)
            {
                _context.PaymentData.Remove(findsCard);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Карта успешно удалена" });
            }

            return BadRequest(new { Message = "Карта не была найдена." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("getcard")]
    [Authorize]
    public async Task<IActionResult> GetUsersCards()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            var UserCards = _context.PaymentData.Where(x => x.userId == userId)
                .Select(x => x.cardNumber).ToList();
            if (UserCards.Any())
            {
                return Ok(new {Cards= UserCards});
            }
            return Ok(new { Cards =  "null" });
            
        }

        return Unauthorized(new { Message = "Не удалось получить данные о пользователе" });
    }
}
using Microsoft.AspNetCore.Mvc;
using GameRankPaymentSystem.Models;
using GameRankPaymentSystem.ValidatorModules;
using GameRankPaymentSystem.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace GameRankPaymentSystem.Controllers;
[ApiController]
[Route("api/addpayment")]
public class AddPaymentController:ControllerBase
{
    private readonly PaymentDBContext _context;

    public AddPaymentController(PaymentDBContext context)
    {
        _context = context;
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
            return BadRequest(new { Message = ex.Message });
        }

    }
}
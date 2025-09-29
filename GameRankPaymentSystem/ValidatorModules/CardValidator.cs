namespace GameRankPaymentSystem.ValidatorModules;
using FluentValidation;
using GameRankPaymentSystem.Models;
public class CardValidator:AbstractValidator<CardData>
{
    public CardValidator()
    {
        RuleFor(x => x.CardNumber).NotEmpty().Length(16 ,16).Matches("^[0-9]{16}$").WithMessage("Номер карты должен содержать минимум 16 символов");
        RuleFor(x => x.CardHolderName).NotEmpty().Matches(".*[a-zA-Z].*")
            .WithMessage("Имя владельца не может содержать такие символы");
        RuleFor(x => x.CardExpiration).NotEmpty().Matches("^[0-9]{4}$");
        RuleFor(x => x.CardSecurityNumber).NotEmpty().Matches("^[0-9]{3}$");
        
    }
}
namespace GameRankPaymentSystem.Models.DTOs;


    public record CardDataForPay
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }
        public decimal Amount { get; set; }
    

    }
    
    public record SaveCard
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }
    
    public record CardData
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardExpiration { get; set; }
        public string CardSecurityNumber { get; set; }

    }

using GameRankPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace GameRankPaymentSystem.Data;

public class PaymentDBContext: DbContext
{
    public PaymentDBContext(DbContextOptions<PaymentDBContext> options) : base(options) {}
    
        public DbSet<PaymentData>  PaymentData { get; set; }
        
    
}
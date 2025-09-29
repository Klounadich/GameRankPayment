
using GameRankPaymentSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAuth(builder.Configuration); // ← Добавьте эту строку
builder.Services.AddAuthorization(); // ← И эту строку
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<PaymentDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PaymentConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost", "http://192.168.0.103")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://192.168.0.103:5003");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");


app.MapControllers(); 


app.Run(); 
using System;
using Microsoft.AspNetCore.Identity;

namespace MockProcessing.Data
{
    public class Transaction
    {
        public Guid Id { get; set; }                    // ID
        public DateTime CreatedAt { get; set; }         // Время поступления платежа
        public DateTime UpdatedAt { get; set; }         // Время проводки (обновления платежа)
        public DateTime PaidAt { get; set; }            // Время оплаты
        
        //public Account Account { get; set; }
        public decimal Amount { get; set; }             // Сумма платежа
        
        public bool Canceled { get; set; } = false;     // Отменен платеж или нет

        public Status Status { get; set; }              // Статус платежа
        //public Code Code { get; set; }
        //public Product Product { get; set; }
        public IdentityUser User { get; set; }
    }
}
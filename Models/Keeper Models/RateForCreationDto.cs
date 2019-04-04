using System;
using System.ComponentModel.DataAnnotations;

namespace MP.Models.Keeper {
    public class RateForCreationDto {
        public decimal Rate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CurrencyId { get; set; }
    }
}
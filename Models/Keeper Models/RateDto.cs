using System;
using System.ComponentModel.DataAnnotations;

namespace MP.Models.Keeper {
    public class RateDto {
        public Guid Id { get; set; }

        public decimal Rate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid CurrencyId { get; set; }
        public string IsoCode { get; set; }
    }
}
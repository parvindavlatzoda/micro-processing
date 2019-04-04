
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MP.Data.Keeper {
    public class CurrencyRate {
        [Key]
        public Guid Id { get; set; }

        public decimal Rate { get; set; }
        public DateTime CreatedAt { get; set; }

       [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }  
        public Guid CurrencyId { get; set; }
    }
}
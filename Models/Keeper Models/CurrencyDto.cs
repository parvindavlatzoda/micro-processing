using System;
using System.ComponentModel.DataAnnotations;

namespace MP.Models.Keeper {
    public class CurrencyDto {
        public Guid Id { get; set; }
        public string IsoCode { get; set; }
        public int IsoNumber { get; set; }
        public string Title { get; set; }
    }
}
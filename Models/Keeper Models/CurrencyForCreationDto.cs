using System;
using System.ComponentModel.DataAnnotations;

namespace MP.Models.Keeper {
    public class CurrencyForCreationDto {
        [Required]
        public string IsoCode { get; set; }
        [Required]
        public int IsoNumber { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
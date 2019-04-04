using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MP.Data.Keeper {
    public class Currency {
        [Key]
        public Guid Id { get; set; }
        public string IsoCode { get; set; }
        public int IsoNumber { get; set; }
        public string Title { get; set; }

        public ICollection<CurrencyRate> Rates { get; set; }
    }
}
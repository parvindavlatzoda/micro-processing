using System;

namespace MP.Models.Keeper {

    public class AggregatedReportDto {
        public DateTime Date { get; set; }
        public decimal AmountInTjs { get; set; }
        public decimal AmountInRub { get; set; }
        public int Quantity { get; set; }
    }
}
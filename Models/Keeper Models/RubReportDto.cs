using System;
using System.ComponentModel.DataAnnotations;

namespace MP.Keeper.Models {
    public class RubReportDto {
        public Guid Id { get; set; }
        public string QpayTransactionId { get; set; }
        public string GatewayTransactionId { get; set; }
        public string ServiceProviderTransactionId { get; set; }
        public decimal AmountInTjs { get; set; }
        public decimal AmountInRub { get; set; }
        public decimal RubRate { get; set; }
        public string Account { get; set; }
        public string ServiceTitle { get; set; }
        public int ServiceUpgId { get; set;}
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime QpayCreatedAt { get; set; }
        public DateTime QpayPayedAt { get; set; }
        public string TerminalNumber { get; set; }
        public string AgentNumber { get; set; }
        public string Status { get; set; }
    }
}
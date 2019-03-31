using System;
using System.ComponentModel.DataAnnotations;

namespace MP.Keeper.Models {
    public class RubReportForCreationDto {
        [Required]
        public string QpayTransactionId { get; set; }
        [Required]
        public string GatewayTransactionId { get; set; }
        [Required]
        public string ServiceProviderTransactionId { get; set; }
        [Required]
        public decimal AmountInTjs { get; set; }
        [Required]
        public decimal AmountInRub { get; set; }
        [Required]
        public decimal RubRate { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string ServiceTitle { get; set; }
        [Required]
        public int ServiceUpgId { get; set;}
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime QpayCreatedAt { get; set; }
        public DateTime QpayPayedAt { get; set; }
        public string TerminalNumber { get; set; }
        public string AgentNumber { get; set; }
    }
}
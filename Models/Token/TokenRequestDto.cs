using System.ComponentModel.DataAnnotations;

namespace MP.Models.Token {
    public class TokenRequestDto {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.Models;

public class RefreshRequest
{
    [Required]
    [MaxLength(100)]
    public string RefreshToken { get; set; } = string.Empty;

}

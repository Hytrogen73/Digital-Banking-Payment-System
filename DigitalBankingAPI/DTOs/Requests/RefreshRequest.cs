using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.DTOs.Requests;

public class RefreshRequest
{
    [Required]
    [MaxLength(100)]
    public string RefreshToken { get; set; } = string.Empty;

}

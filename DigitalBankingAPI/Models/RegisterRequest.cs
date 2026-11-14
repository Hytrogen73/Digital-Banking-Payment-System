using System.ComponentModel.DataAnnotations;

namespace DigitalBankingAPI.Models;

public class RegisterRequest
{
    [Required]
    [StringLength(50, MinimumLength =3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}

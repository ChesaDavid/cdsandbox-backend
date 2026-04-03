namespace cdsandbox.Backend.DTOs;

public class LoginRequest
{
    public string Emali { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
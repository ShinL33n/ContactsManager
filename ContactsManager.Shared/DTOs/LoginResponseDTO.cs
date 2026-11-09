namespace ContactsManager.Shared.DTOs;

public class LoginResponseDTO
{
    public string Token { get; set; } = "";
    public DateTime ExpireTime { get; set; }
}

namespace APITON.DTOs;

#nullable disable
public class UserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; internal set; }
}

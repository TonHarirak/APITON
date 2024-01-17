namespace APITON.DTOs;

#nullable disable
public class UserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; internal set; }
    public string PhotoUrl { get; set; }
    public string Aka { get; set; }

    public string Gender { get; set; }

}

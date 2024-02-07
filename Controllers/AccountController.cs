
using API.Controllers;
using APITON.DTOs;
using APITON.Entities;
using APITON.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITON.Controllers;

public class AccountController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager; //<--

    public AccountController(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService) //ctrl + . select create and assign field
    {
        _mapper = mapper;

        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await isUserExists(registerDto.Username!)) return BadRequest("username is already exists");
        var user = _mapper.Map<AppUser>(registerDto);
        user.UserName = registerDto.Username!.Trim().ToLower();
        var appUser = await _userManager.CreateAsync(user, registerDto.Password!);//
        if (!appUser.Succeeded) return BadRequest(appUser.Errors);//<--


        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            Aka = user.Aka,
            Gender = user.Gender,
        };

    }

    private async Task<bool> isUserExists(string username) =>
        await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
    [HttpPost("login")]

    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {

        var user = await _userManager.Users //<--
                        .Include(photo => photo.Photos).SingleOrDefaultAsync(user =>
                            user.UserName == loginDto.UserName!.ToLower()); //<--
        if (user is null) return Unauthorized("invalid username");
        var appUser = await _userManager.CheckPasswordAsync(user, loginDto.Password!); //<--
        if (!appUser) return BadRequest("invalid password"); //<--
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user),
            PhotoUrl = user.Photos.FirstOrDefault(photo => photo.IsMain)?.Url
            ,
            Aka = user.Aka,
            Gender = user.Gender,
        };
    }
}
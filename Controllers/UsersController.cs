
using System.Security.Claims;
using API.Controllers;
using APITON.Data;
using APITON.DTOs;
using APITON.Entities;
using APITON.Extensions;
using APITON.Interface;
using APITON.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APITON.Controller;
//[Authorize]
public class UsersController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly IUserRepository _userRepository;
    public UsersController(IImageService imageService, IMapper mapper, IUserRepository userRepository)
    {
        _imageService = imageService;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {

        return Ok(await _userRepository.GetMembersAsync());
    }
    //public async Task<ActionResult<MemberDto?>> GetUser(int id) => await _userRepository.GetUserByIdAsync(id);
    [AllowAnonymous]

    [HttpGet("{id}")]

    public async Task<ActionResult<MemberDto?>> GetUser(int id)
    {
        var appUser = await _userRepository.GetUserByIdAsync(id);
        return _mapper.Map<MemberDto>(appUser);


    }

    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto?>> GetUserByUserName(string username)
    {

        return await _userRepository.GetMemberByUserNameAsync(username);

    }

    private async Task<AppUser?> _GetUser()
    {
        var username = User.GetUsername();
        if (username is null) return null;
        return await _userRepository.GetUserByUserNameAsync(username);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUserProfile(MemberUpdateDto memberUpdateDto)
    {
        var appUser = await this._GetUser();
        if (appUser is null) return NotFound();

        //var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //if (username is null) return Unauthorized();

        _mapper.Map(memberUpdateDto, appUser);
        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update user profile!");
    }

    [HttpPost("add-image")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await _GetUser();
        if (user is null) return NotFound();

        var result = await _imageService.AddImageAsync(file);
        if (result.Error is not null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        if (user.Photos.Count == 0) photo.IsMain = true;

        user.Photos.Add(photo);
        if (await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);
        return BadRequest("Something has gone wrong!");
    }

}

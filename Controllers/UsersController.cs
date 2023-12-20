
using API.Controllers;
using APITON.Data;
using APITON.DTOs;
using APITON.Entities;
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
    private readonly IUserRepository _userRepository;
    public UsersController(IMapper mapper, IUserRepository userRepository)
    {
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

}

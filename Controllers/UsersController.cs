
using API.Controllers;
using APITON.Data;
using APITON.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controller;
//[Authorize]
public class UsersController : BaseApiController
{
    private readonly DataContext _dataContext;
    public UsersController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return await _dataContext.Users.ToListAsync();
    }
    [HttpGet("{id}")]

    public async Task<ActionResult<AppUser?>> GetUser(int id)
    {
        return await _dataContext.Users.FindAsync(id);
    }
}

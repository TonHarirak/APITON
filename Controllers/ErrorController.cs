using System;
using API.Controllers;
using APITON.Data;
using APITON.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITON.ClassLibrary1;

#nullable disable
public class ErrorController : BaseApiController
{
    private readonly DataContext _dataContext;

    public ErrorController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "xxx";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var user = _dataContext.Users.Find(-1);
        if (user is null) return NotFound();
        return user;
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        try
        {
            var user = _dataContext.Users.Find(-1);
            var stringUser = user.ToString();//can not turn null to string = no reference exception
            return stringUser;
        }
        catch (System.Exception e)
        {

            return StatusCode(500, e.ToString);
        }

    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("illegal request");
    }
}



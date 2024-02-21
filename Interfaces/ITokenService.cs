using System;
using APITON.Entities;

namespace APITON.Interface;

public interface ITokenService

{
    Task<string> CreateToken(AppUser user);

}

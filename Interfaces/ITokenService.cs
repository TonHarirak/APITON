using System;
using APITON.Entities;

namespace APITON.Interface;

public interface ITokenService
{
    string CreateToken(AppUser user);
}

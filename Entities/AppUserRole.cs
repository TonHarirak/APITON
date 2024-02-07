using System;
using APITON.Entities;
using Microsoft.AspNetCore.Identity;

namespace APITON.Entities;
#nullable disable
public class AppUserRole : IdentityUserRole<int>
{
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}

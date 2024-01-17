using System;
using APITON.DTOs;
using APITON.Entities;
using APITON.Helpers;

namespace APITON.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUserNameAsync(string username);
    // Task<IEnumerable<AppUser>> GetUsersAsync();
    //Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
    Task<MemberDto?> GetMemberByUserNameAsync(string username);
}
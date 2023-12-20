﻿using APITON.DTOs;
using APITON.Entities;
using APITON.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace APITON.Data;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public UserRepository(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }

    public async Task<MemberDto?> GetMemberByUserNameAsync(string username)
    {
        return await _dataContext.Users
            .Where(user => user.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await _dataContext.Users
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await _dataContext.Users.FindAsync(id);
        //return await _dataContext.Users
        //.Where(user => user.Id == id)
        //.ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        //.SingleOrDefaultAsync();
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        return await _dataContext.Users
        .Include(user => user.Photos)
        .SingleOrDefaultAsync(user => user.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _dataContext.Users
          .Include(user => user.Photos)
          .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user) => _dataContext.Entry(user).State = EntityState.Modified;




}
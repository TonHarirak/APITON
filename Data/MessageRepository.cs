using System;
using APITON.DTOs;
using APITON.Entities;
using APITON.Helpers;
using APITON.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace APITON.Data;

public class MessageRepository : IMessageRepository

{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public MessageRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }
    public void AddMessage(Message message) => _dataContext.Messages.Add(message);

    public void DeleteMessage(Message message) => _dataContext.Messages.Remove(message);

    public async Task<Message?> GetMessage(int id) => await _dataContext.Messages.FindAsync(id);

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string thisUserName, string recipientUserName)
    {
        var messages = await _dataContext.Messages
                      .Include(ms => ms.Sender).ThenInclude(user => user!.Photos)
                      .Include(ms => ms.Recipient).ThenInclude(user => user!.Photos)
                      .Where(ms =>
                          (ms.RecipientUsername == thisUserName && ms.SenderUsername == recipientUserName) ||
                          (ms.RecipientUsername == recipientUserName && ms.SenderUsername == thisUserName)
                      )
                                              //.OrderByDescending(ms => ms.DateSent)
                                              .OrderBy(ms => ms.DateSent)

                      .ToListAsync();

        var unreadMessages = messages
      .Where(ms =>
            (ms.RecipientUsername == thisUserName
              && ms.IsRecipientDeleted == false
              && ms.SenderUsername == recipientUserName) ||
            (ms.RecipientUsername == recipientUserName
              && ms.IsSenderDeleted == false
              && ms.SenderUsername == thisUserName)
        )
      .ToList();

        if (unreadMessages.Any())
        {
            foreach (var ms in unreadMessages)
                ms.DateRead = DateTime.UtcNow;
            await _dataContext.SaveChangesAsync();
        }
        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }
    public async Task<PageList<MessageDto>> GetUserMessages(MessageParams messageParams)
    {
        var query = _dataContext.Messages.OrderByDescending(ms => ms.DateSent).AsQueryable();
        query = messageParams.Label switch
        {
            "Inbox" => query.Where(ms => ms.RecipientUsername == messageParams.Username &&
                                          ms.IsRecipientDeleted == false),
            "Sent" => query.Where(ms => ms.SenderUsername == messageParams.Username &&
                                          ms.IsSenderDeleted == false),
            _ => query.Where(ms => ms.RecipientUsername == messageParams.Username &&
                                          ms.IsRecipientDeleted == false && ms.DateRead == null)
        };
        var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);
        return await PageList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
    }
    public async Task<bool> SaveAllAsync() => await _dataContext.SaveChangesAsync() > 0;
}


using APITON.DTOs;
using APITON.Entities;
using APITON.Helpers;

namespace APITON.Interfaces;

public interface IlikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);

    Task<AppUser> GetUser(int userId);

    //Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);

    Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams);

}

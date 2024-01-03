using APITON.DTOs;
using APITON.Entities;
using APITON.Extensions;
using AutoMapper;

namespace APITON.Helpers;

#nullable disable
public class AutoMapperUserProfiles : Profile
{
    public AutoMapperUserProfiles()
    {
        CreateMap<AppUser, MemberDto>()
        .ForMember(
                user => user.Age,
                opt => opt.MapFrom(user => user.BirthDate.CalculateAge())
            )
        .ForMember(
         user => user.MainPhotoUrl,
         opt => opt.MapFrom(
         user => user.Photos.FirstOrDefault(photo => photo.IsMain).Url
        )
        );
        CreateMap<Photo, PhotoDto>();
        CreateMap<MemberUpdateDto, AppUser>();

    }
}

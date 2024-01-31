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
        CreateMap<Message, MessageDto>()
                    .ForMember(
                        msdto => msdto.SenderPhotoUrl,
                        opt => opt.MapFrom(
                                ms => ms.Sender.Photos.FirstOrDefault(photo => photo.IsMain).Url
                            )
                    )
                    .ForMember(
                        msdto => msdto.RecipientPhotoUrl,
                        opt => opt.MapFrom(
                                ms => ms.Recipient.Photos.FirstOrDefault(photo => photo.IsMain).Url
                            )
                    );

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
        CreateMap<RegisterDto, AppUser>();
    }
}

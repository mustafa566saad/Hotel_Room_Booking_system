using AutoMapper;
using Hotel_Room_Booking_system.DTOs;

namespace Hotel_Room_Booking_system.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<RoomDTO, Room>();

            CreateMap<Room, ReturmedRoomsDTO>()
                .ForMember(dest => dest.PricePerDay, opt => opt.MapFrom(src => src.BasePrice));

        }
    }
}

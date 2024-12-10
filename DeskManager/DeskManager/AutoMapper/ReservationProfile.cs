using AutoMapper;
using DeskManager.Models;
using DeskManager.Models.DTO;

namespace DeskManager.AutoMapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>();
        }
    }
}

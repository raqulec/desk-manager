using AutoMapper;
using DeskManager.Models;
using DeskManager.Models.DTO;

namespace DeskManager.AutoMapper
{
    public class DeskProfile : Profile
    {
        public DeskProfile()
        {
            CreateMap<Desk, DeskDto>();
        }
    }
}

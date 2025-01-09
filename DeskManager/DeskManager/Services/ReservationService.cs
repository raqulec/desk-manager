using DeskManager.Models;
using DeskManager.Repository;
using DeskManager.Utils;

namespace DeskManager.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        //public async Task<Reservation> CreateUser(Reservation request)
        //{
        //    //return await _reservationRepository.AddReservation();
        //}
    }
}

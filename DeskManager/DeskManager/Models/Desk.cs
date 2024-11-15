namespace DeskManager.Models
{
    public class Desk
    {
        public int DeskId { get; set; }
        public int DeskNumber { get; set; }
        public string RoomName { get; set; }
        public bool IsAvailable { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public bool IsAvailableOnDate(DateTime date)
        {
            return !Reservations.Any(r => r.ReservationDate != null && r.ReservationDate.Date == date.Date);
        }
    }
}

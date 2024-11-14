namespace DeskManager.Models
{
    public class Desk
    {
        public int DeskNumber { get; set; }
        public string RoomName { get; set; }
        public bool IsAvailable { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}

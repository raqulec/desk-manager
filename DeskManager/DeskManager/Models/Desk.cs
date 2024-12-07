namespace DeskManager.Models
{
    public class Desk
    {
        public int Id { get; set; }
        public int DeskNumber { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = [];
    }
}

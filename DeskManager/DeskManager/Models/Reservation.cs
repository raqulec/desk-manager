namespace DeskManager.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservedBy { get; set; } = string.Empty;
        public int DeskId { get; set; }
        public Desk? Desk { get; set; }
        public bool IsAvailable { get; set; }
    }
}

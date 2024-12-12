namespace DeskManager.Models
{
    public class DeskFilter
    {
        public string DeskNumber { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = false;
        public string ReservedBy { get; set; } = string.Empty;
        public DateTime? ReservationDate { get; set; }
    }
}

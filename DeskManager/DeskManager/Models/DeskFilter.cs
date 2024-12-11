namespace DeskManager.Models
{
    public class DeskFilter
    {
        public int? DeskNumber { get; set; }
        public string? RoomName { get; set; }
        public bool? IsAvailable { get; set; }
        public string? ReservedBy { get; set; }
        public DateTime? ReservationDate { get; set; }
    }
}

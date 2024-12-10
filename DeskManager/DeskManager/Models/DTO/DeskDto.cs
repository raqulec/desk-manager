namespace DeskManager.Models.DTO
{
    public class DeskDto
    {
        public int Id { get; set; }
        public int DeskNumber { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public ICollection<ReservationDto> Reservations { get; set; } = [];
    }
}

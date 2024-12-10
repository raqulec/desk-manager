namespace DeskManager.Models.DTO
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservedBy { get; set; } = string.Empty;
    }
}

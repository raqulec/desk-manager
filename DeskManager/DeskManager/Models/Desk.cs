using System.ComponentModel.DataAnnotations;

namespace DeskManager.Models
{
    public class Desk
    {
        public int Id { get; set; }
        [Required]
        public string DeskNumber { get; set; } = string.Empty;
        [Required]
        public string RoomName { get; set; } = string.Empty;
    }
}

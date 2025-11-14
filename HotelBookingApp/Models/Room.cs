using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        public string? Description { get; set; }
    }
}

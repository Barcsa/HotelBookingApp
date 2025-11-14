using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public User? User { get; set; }
        public Room? Room { get; set; }
    }
}

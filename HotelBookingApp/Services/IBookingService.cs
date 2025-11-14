using HotelBookingApp.Models;

namespace HotelBookingApp.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByUserAsync(int userId);
        Task<IEnumerable<Booking>> GetBookingsByRoomAsync(int roomId);

        Task<Booking?> GetBookingByIdAsync(int bookingId);

        Task<(bool Success, string Message)> CreateBookingAsync(Booking booking);
        Task<(bool Success, string Message)> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}

using HotelBookingApp.Models;

namespace HotelBookingApp.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<Booking>> GetBookingsByUserAsync(int userId);
        Task<IEnumerable<Booking>> GetBookingsByRoomAsync(int roomId);

        Task<Booking?> GetBookingByIdAsync(int bookingId);

        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int bookingId);

        Task<bool> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate);
        Task<bool> IsRoomAvailableForUpdateAsync(int roomId, DateTime startDate, DateTime endDate, int bookingIdToExclude);

        Task SaveChangesAsync();
    }
}

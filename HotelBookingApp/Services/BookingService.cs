using HotelBookingApp.Models;
using HotelBookingApp.Repositories;

namespace HotelBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
            => await _bookingRepository.GetAllBookingsAsync();

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(int userId)
            => await _bookingRepository.GetBookingsByUserAsync(userId);

        public async Task<IEnumerable<Booking>> GetBookingsByRoomAsync(int roomId)
            => await _bookingRepository.GetBookingsByRoomAsync(roomId);

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
            => await _bookingRepository.GetBookingByIdAsync(bookingId);

        public async Task<(bool Success, string Message)> CreateBookingAsync(Booking booking)
        {
            var isAvailable = await _bookingRepository.IsRoomAvailableAsync(
                booking.RoomId,
                booking.StartDate,
                booking.EndDate
            );

            if (!isAvailable)
                return (false, "The room is already booked for this time interval.");

            await _bookingRepository.AddBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return (true, "Booking created successfully.");
        }

        public async Task<(bool Success, string Message)> UpdateBookingAsync(Booking booking)
        {
            var isAvailable = await _bookingRepository.IsRoomAvailableForUpdateAsync(
                booking.RoomId,
                booking.StartDate,
                booking.EndDate,
                booking.Id
            );

            if (!isAvailable)
                return (false, "The room is already booked for this time interval.");

            await _bookingRepository.UpdateBookingAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return (true, "Booking updated successfully.");
        }

        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            await _bookingRepository.DeleteBookingAsync(bookingId);
            await _bookingRepository.SaveChangesAsync();
            return true;
        }
    }
}

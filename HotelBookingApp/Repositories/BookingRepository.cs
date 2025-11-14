using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
            => await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .ToListAsync();

        public async Task<IEnumerable<Booking>> GetBookingsByUserAsync(int userId)
            => await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Room)
                .ToListAsync();

        public async Task<IEnumerable<Booking>> GetBookingsByRoomAsync(int roomId)
            => await _context.Bookings
                .Where(b => b.RoomId == roomId)
                .Include(b => b.User)
                .ToListAsync();

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
            => await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await GetBookingByIdAsync(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime startDate, DateTime endDate)
        {
            return !await _context.Bookings
                .AnyAsync(b =>
                    b.RoomId == roomId &&
                    b.StartDate < endDate &&
                    startDate < b.EndDate
                );
        }

        public async Task<bool> IsRoomAvailableForUpdateAsync(int roomId, DateTime startDate, DateTime endDate, int bookingIdToExclude)
        {
            return !await _context.Bookings
                .AnyAsync(b =>
                    b.RoomId == roomId &&
                    b.Id != bookingIdToExclude &&
                    b.StartDate < endDate &&
                    startDate < b.EndDate
                );
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

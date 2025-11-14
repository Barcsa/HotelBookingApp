using HotelBookingApp.Models;
using HotelBookingApp.Repositories;

namespace HotelBookingApp.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
            => await _roomRepository.GetAllRoomsAsync();

        public async Task<Room?> GetRoomByIdAsync(int roomId)
            => await _roomRepository.GetRoomByIdAsync(roomId);

        public async Task<bool> CreateRoomAsync(Room room)
        {
            await _roomRepository.AddRoomAsync(room);
            await _roomRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            await _roomRepository.UpdateRoomAsync(room);
            await _roomRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoomAsync(int roomId)
        {
            await _roomRepository.DeleteRoomAsync(roomId);
            await _roomRepository.SaveChangesAsync();
            return true;
        }
    }
}

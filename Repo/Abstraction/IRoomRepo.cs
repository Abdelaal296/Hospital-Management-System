using HospitalSystem.Models;

namespace HospitalSystem.Repo.Abstraction
{
    public interface IRoomRepo
    {
        bool AddRoom(Room room);
        Room GetRoomById(int id);
        List<Room> GetAllRooms();
        bool UpdateRoom(Room room);
        bool DeleteRoom(int id);
    }
}

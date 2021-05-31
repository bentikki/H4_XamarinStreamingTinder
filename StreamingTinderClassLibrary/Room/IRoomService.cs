using StreamingTinderClassLibrary.StreamingService;
using StreaminTinderClassLibrary.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamingTinderClassLibrary.Rooms
{
    public interface IRoomService
    {
        List<IRoom> GetUserRoomAccess(IUser user);
        Task<List<IRoom>> GetUserRoomAccessAsync(IUser user);
        List<IRoom> GetUserRoomsOwned(IUser user);
        Task<List<IRoom>> GetUserRoomsOwnedAsync(IUser user);
        IRoom CreateRoom(IUser owner, string roomName, List<IStreamingPlatform> services);
        Task<IRoom> CreateRoomAsync(IUser owner, string roomName, List<IStreamingPlatform> services);
        bool AddUserToRoom(IRoom room, IUser user);
        Task<bool> AddUserToRoomAsync(IRoom room, IUser user);
        bool AddStreamingServiceToRoom(IRoom room, IStreamingPlatform service);
        Task<bool> AddStreamingServiceToRoomAsync(IRoom room, IStreamingPlatform service);
        IRoom GetRoom(int id);
        Task<IRoom> GetRoomAsync(int id);
        IRoom GetRoomByRoomKey(string roomKey);
        Task<IRoom> GetRoomByRoomKeyAsync(string roomKey);
    }
}

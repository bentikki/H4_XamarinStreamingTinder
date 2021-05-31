using StreamingTinderClassLibrary.Room;
using StreamingTinderClassLibrary.Room.DataAccess;
using StreamingTinderClassLibrary.StreamingService;
using StreamingTinderClassLibrary.StreamingService.DataAccess;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.DataAccess;

namespace StreamingTinderClassLibrary
{
    public static class ServiceFactory
    {
        private static string ApiKey { get; set; } = @"https://84c7c23a461f.ngrok.io" + @"/api/";

        public static IUserService GetUserService()
        {
            return new UserService(new DAOUserAPI( ApiKey));
        }
        public static IRoomService GetRoomService()
        {
            return new RoomService( new DAORoomAPI(ApiKey));
        }

        public static IStreamingPlatformService GetStreamingPlatformService()
        {
            return new StreamingPlatformService(new DAOStreamingPlatformAPI(ApiKey));
        }
    }
}


using StreamingTinderClassLibrary.Rooms;
using StreamingTinderClassLibrary.Rooms.DataAccess;
using StreamingTinderClassLibrary.StreamingService;
using StreamingTinderClassLibrary.StreamingService.DataAccess;
using StreaminTinderClassLibrary.Users;
using StreaminTinderClassLibrary.Users.DataAccess;

namespace StreamingTinderClassLibrary
{
    public static class ServiceFactory
    {
        private static string ApiKey { get; set; } = @"https://4ed90f21bbec.ngrok.io" + @"/api/";
        internal static byte RoomKeyLength { get; } = 12;

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

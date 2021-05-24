using StreaminTinderClassLibrary.Authentication;

namespace StreaminTinderClassLibrary.Hashing.Models
{
    class HashedUser : IHashedUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

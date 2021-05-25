using StreaminTinderClassLibrary.Hashing;
using StreaminTinderClassLibrary.Users.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreaminTinderClassLibrary.Users
{
    /// <summary>
    /// Facade Service used to handle user CRUD
    /// </summary>
    public class UserService
    {
        private IUserDAO _userDAO;
        private HashingService _hashingService;

        public IUser CurrentUser { get; private set; }
        public bool IsUserLoggedIn { get; private set; }

        public UserService()
        {
            this._userDAO = new DAOUserAPI();

            // Setup hashing service.
            this._hashingService = new HashingService(new HashingSettings(HashingMethodType.SHA256));
        }

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">ID of requested user.</param>
        /// <returns>IUser object correosponding to provided ID</returns>
        public IUser GetUser(int id)
        {
            if (id < 1) throw new ArgumentException("id must be at least 1", "id");

            IUser selectedUser;

            selectedUser = this._userDAO.Get(id);

            return selectedUser;
        }
    
        /// <summary>
        /// Creates new user from provided IUser object.
        /// </summary>
        /// <param name="user">IUser object to be used as creation template</param>
        /// <returns>The newly created IUser object</returns>
        public IUser CreateNewUser(IUser user)
        {
            // Verify checks
            if (user == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email must not be null or empty", "Email");
            if (string.IsNullOrEmpty(user.Username)) throw new ArgumentException("Username must not be null or empty", "Username");
            if (string.IsNullOrEmpty(user.Password)) throw new ArgumentException("Password must not be null or empty", "Password");


            IUser createdUser;
            IHashedUser hashedUser;

            // Create hashed user.
            try
            {
                hashedUser = this._hashingService.CreateHashedUser(user.Username, user.Password);
            }
            catch (Exception e)
            {
                throw new Exception("Hashed user could not be created.", e);
            }

            // Set Hashed user info to user object.
            user.Password = hashedUser.Password;
            user.Salt = hashedUser.Salt;

            // Create user 
            createdUser = this._userDAO.Create(user);

            return createdUser;
        }

        /// <summary>
        /// Verifies the user by email and password. 
        /// Return true if user can be verified, returns false if not.
        /// </summary>
        /// <param name="user">User to verify - Must include Email and Password</param>
        /// <returns>Boolean value based on validity of user provided login credentials</returns>
        public bool VerifyUser(IUser user)
        {
            // Boolean value based on validity of user provided login credentials 
            bool userLoginSuccess = false;

            // Verify checks
            if (user == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email must not be null or empty", "Email");
            if (string.IsNullOrEmpty(user.Password)) throw new ArgumentException("Password must not be null or empty", "Password");

            // Verify user be hashing service.
            userLoginSuccess = this._userDAO.VerifyUserLogin(user);

            return userLoginSuccess;
        }

        /// <summary>
        /// Returns user corrosponding to provided email string - if a user with corrosponding email exists.
        /// </summary>
        /// <param name="email">Email of needed user</param>
        /// <returns>User with provided email</returns>
        public IUser GetUserByEmail(string email)
        {
            IUser selectedUser;

            // Verify checks
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email must not be null or empty", "Email");

            // User object from DAO
            selectedUser = this._userDAO.GetByString("email", email);

            return selectedUser;
        }

        /// <summary>
        /// Returns user corrosponding to provided email string - if a user with corrosponding email exists.
        /// </summary>
        /// <param name="email">Email of needed user</param>
        /// <returns>User with provided email</returns>
        public bool DeleteUser(int id)
        {
            bool deletedUserSuccess = false;

            // Verify checks
            if (id <= 0) throw new ArgumentException("id must be above 0", "id");

            // User object from DAO
            deletedUserSuccess = this._userDAO.Delete(id);

            return deletedUserSuccess;
        }

    }
}

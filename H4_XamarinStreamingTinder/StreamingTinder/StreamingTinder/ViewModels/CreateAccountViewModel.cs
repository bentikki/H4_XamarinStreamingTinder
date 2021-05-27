using StreamingTinder.Views;
using StreamingTinderClassLibrary.Validator;
using StreaminTinderClassLibrary.Users;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StreamingTinder.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private IUserService _userService = DependencyService.Get<IUserService>();

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                SetProperty(ref email, value);
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                SetProperty(ref password, value);
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                SetProperty(ref username, value);
            }
        }

        public ICommand ClickCreateButton { get; }


        public CreateAccountViewModel()
        {
            this.Title = "Create user";

            ClickCreateButton = new Command(CreateButtonClicked);
        }

        /// <summary>
        /// "Create" - button clicked.
        /// Validates and creates new user.
        /// Redirect to Home page if successfull.
        /// </summary>
        public async void CreateButtonClicked()
        {
            // Run validation on input fields.
            string[] emailErrors = DefaultValidator.ValidEmailCreateNew(this.Email);
            string[] passwordErrors = DefaultValidator.ValidPassword(this.Password);
            string[] usernameErrors = DefaultValidator.ValidUsername(this.Username);

            // Check for errors
            if(emailErrors.Length == 0 && passwordErrors.Length == 0 && usernameErrors.Length == 0)
            {
                // No errors
                // Create user in database
                try
                {
                    await this._userService.CreateNewUserAsync(Email, Password, Username);

                    // Redirect to login page
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AboutPage(), true);
                }
                catch (Exception e)
                {
                    // An unexpected error occured, so user could not be created.
                    // Show generic error message to user.
                    ShowAlert("An error occured. User could not be created.");
                }

            }
            else
            {
                // Errors in user input.
                // Display errors.
                string errorMessage = "Invalid user data:" + System.Environment.NewLine;

                foreach (var error in emailErrors)
                {
                    errorMessage += System.Environment.NewLine + error;
                }
                foreach (var error in passwordErrors)
                {
                    errorMessage += System.Environment.NewLine + error;
                }
                foreach (var error in usernameErrors)
                {
                    errorMessage += System.Environment.NewLine + error;
                }

                ShowAlert(errorMessage);

                // Color fields with errors.
            }
        }

    }
}
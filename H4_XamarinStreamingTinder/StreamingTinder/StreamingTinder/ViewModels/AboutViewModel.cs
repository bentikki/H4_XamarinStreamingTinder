using Acr.UserDialogs;
using StreamingTinder.Views;
using StreamingTinderClassLibrary.Validator;
using StreaminTinderClassLibrary.Users;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StreamingTinder.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

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

        public ICommand ClickLoginButton { get; }
        public ICommand ClickCreateNewButton { get; }

        public AboutViewModel()
        {
            // Set dependencies.
            this._userService = DependencyService.Get<IUserService>();

            // Check if user is logged in - Redirect if true
            if (this._userService.IsUserLoggedIn)
                RedirectToMainPage();

            this.Title = "Couch Potato";
            ClickLoginButton = new Command(LoginButtonClicked);
            ClickCreateNewButton = new Command(CreateNewButtonClicked);
        }

        /// <summary>
        /// Login button click event.
        /// </summary>
        public async void LoginButtonClicked()
        {
            using (UserDialogs.Instance.Loading("Validating..."))
            {
                string[] emailErrors = DefaultValidator.ValidEmailLogin(this.Email);
                string[] passwordErrors = DefaultValidator.ValidPassword(this.Password);

                if (emailErrors.Length == 0 && passwordErrors.Length == 0)
                {
                    using (UserDialogs.Instance.Loading("Logging in..."))
                    {
                        try
                        {
                            // Login values are valid
                            // Proceed to login validation
                            bool loginSucceed = await this._userService.LoginUserAsync(this.Email, this.Password);

                            if (loginSucceed)
                            {
                                // User login was successfull
                                // Save user data to local DB, for login after app shutdown.
                                await App.LocalUserDatabase.SaveLocalUserDataAsync(new Models.UserLocal(Email, Password));

                                // Redirect to main page
                                this.RedirectToMainPage();

                            }
                            else
                            {
                                // User login was not successfull
                                // Empty input fields and display error

                                this.Email = "";
                                this.Password = "";

                                ShowAlert("Wrong email or password.");
                            }
                        }
                        catch
                        {
                            ShowAlert("An unkown error occured. Please try again.");
                        }
                    }
                }
                else
                {
                    // Login values are not valid
                    // Display error message
                    string errorMessage = "An error occured during login" + System.Environment.NewLine;

                    foreach (var error in emailErrors)
                    {
                        errorMessage += System.Environment.NewLine + error;
                    }
                    foreach (var error in passwordErrors)
                    {
                        errorMessage += System.Environment.NewLine + error;
                    }

                    ShowAlert(errorMessage);
                }
            }
            

        }

        /// <summary>
        /// "Create new account" - button clicked.
        /// Redirects to create new - page
        /// </summary>
        public async void CreateNewButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CreateAccountPage(), true);
        }

    }
}
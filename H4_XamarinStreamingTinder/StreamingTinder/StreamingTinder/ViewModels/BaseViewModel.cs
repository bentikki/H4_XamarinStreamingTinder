using Acr.UserDialogs;
using StreamingTinder.Models;
using StreamingTinder.Services;
using StreamingTinder.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace StreamingTinder.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected async void ShowAlert(string alertString)
        {

            this.ShowAlert("Error", alertString);
        }
        protected async void ShowAlert(string alertHeading, string alertString)
        {
            AlertConfig config = new AlertConfig();
            config.Title = alertHeading;
            config.Message = alertString;
            config.OkText = "Got it";

            await UserDialogs.Instance.AlertAsync(config);
            //await App.Current.MainPage.DisplayAlert(alertHeading, alertString, "OK");
        }

        protected void RedirectToMainPage()
        {
            //Application.Current.MainPage = new NavigationPage();
            Application.Current.MainPage = new DashboardPage();
        }

        protected void RedirectToLoginPage()
        {
            //Application.Current.MainPage = new NavigationPage(new AboutPage());
            Application.Current.MainPage = new AboutPage();

        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

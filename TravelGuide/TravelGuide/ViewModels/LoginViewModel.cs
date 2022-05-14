﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelGuide.Intefaces;
using TravelGuide.Resources.Resx;
using TravelGuide.Services;
using TravelGuide.Views;
using WarehouseMobile.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace TravelGuide.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Declarations

        ///// <summary>
        ///// Хелпър за показване на съобшения
        ///// </summary>
        //private readonly IAlert alert;

        ///// <summary>
        ///// Помощен клас за потребителите в базата
        ///// </summary>
        //private readonly IUsersHelper usersHelper;

        ///// <summary>
        ///// Клас за запис на грешки
        ///// </summary>
        //private readonly IErrorLogger errorLogger;

        /// <summary>
        /// избрания потребител
        /// </summary>
        private User selectedUser;

        /// <summary>
        /// въведената парола
        /// </summary>
        private string password;

        /// <summary>
        /// въведената парола
        /// </summary>
        private string username;

        /// <summary>
        /// Потребителите в базата
        /// </summary>
        private IList<User> users;

        /// <summary>
        /// Команда за вход
        /// </summary>
        private ICommand loginCommand;

        private ICommand registerCommand;

        /// <summary>
        /// Команда при показване на екрана
        /// </summary>
        private ICommand onAppearingCommand;

        #endregion

        #region Constructors

        public LoginViewModel()
        {
        }

        #endregion

        #region Properties

        #region Commands

        /// <summary>
        /// Команда за вход
        /// </summary>
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new ExtendedCommand(Login));

        public ICommand RegisterCommand => registerCommand ?? (registerCommand = new ExtendedCommand(NavigateToRegisterScreen));

        /// <summary>
        /// Команда при показване на екрана
        /// </summary>
        public ICommand OnAppearingCommand => onAppearingCommand ?? (onAppearingCommand = new ExtendedCommand(OnAppearing));

        #endregion

        /// <summary>
        /// Избрания потребител
        /// </summary>
        public User SelectedUser
        {
            get
            {
                if (selectedUser == null)
                {
                    selectedUser = Users?.FirstOrDefault();
                }

                return selectedUser;
            }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Въведената парола
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Въведената парола
        /// </summary>
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Списък с възможни потребители
        /// </summary>
        public IList<User> Users
        {
            get
            {
                return users;
            }
            private set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Презарежда потребителите при всяко отваряне на екрана
        /// </summary>
        /// <param name="obj"></param>
        private void OnAppearing(object obj)
        {

            Settings.Settings.LoggedUserId = -1;
            Username = null;
            Password = null;
        }

        ///// <summary>
        ///// Презарежда данните
        ///// </summary>
        //private void LoadUsers()
        //{
        //    Task.Run(async () =>
        //    {
        //        var users = (await usersHelper.GetUsersAsync()) as List<User>;

        //        Users = users.Count > 1 ? new List<User>(users.Where(u => u.Id > 1).OrderBy(u => u.Name)) : new List<User>(users);

        //        SelectedUser = Users.FirstOrDefault();
        //        Password = string.Empty;

        //        SettingsManager.IsLoggedIn = false;
        //    }).Wait();
        //}

        /// <summary>
        /// Валидира потребителя и извършва вход в приложението
        /// </summary>
        /// <param name="obj"></param>
        private async void Login(object obj)
        {
            if (!Validate())
            {
                return;
            }

            Settings.Settings.LoggedUserId = 1;

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

            //try
            //{
            //    IsBusy = true;

            //    if (Password == usersHelper.GetTemporaryServicePassword())
            //    {
            //        SettingsManager.LoggedUserID = 1;
            //        SettingsManager.IsLoggedIn = true;
            //        NavigateToMainScreen();
            //    }
            //    else if (SelectedUser.Password == CryptoHelper.Encrypt(Password.Equals(string.Empty) ? " " : Password))
            //    {
            //        SettingsManager.LoggedUserID = SelectedUser.Id.Value;
            //        SettingsManager.IsLoggedIn = true;
            //        NavigateToMainScreen();
            //    }
            //    else
            //    {
            //        await alert.ShowWarningAsync(AppResources.strInvalidPassword);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await errorLogger.LogException(ex);
            //}
            //finally
            //{
            //    IsBusy = false;
            //}
        }

        /// <summary>
        /// Навигира към началния екран
        /// </summary>
        private async void NavigateToMainScreen()
        {
            MessagingCenter.Send(this, "ChangedUser");
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private async void NavigateToRegisterScreen(object _)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        private bool Validate()
        {
            if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                DependencyService.Resolve<IMessage>().LongAlert(AppResources.strEnterUsernameAndPassword);
                return false;
            }
            return true;
        }

        

        #endregion
    }
}

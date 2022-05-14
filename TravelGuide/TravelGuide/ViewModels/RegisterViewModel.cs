using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using TravelGuide.Intefaces;
using TravelGuide.Resources.Resx;
using TravelGuide.Services;
using TravelGuide.Views;
using WarehouseMobile.Commands;
using Xamarin.Forms;

namespace TravelGuide.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Declarations

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
        private string confirmPassword;

        /// <summary>
        /// въведената парола
        /// </summary>
        private string username;

        /// <summary>
        /// въведената парола
        /// </summary>
        private string email;

        /// <summary>
        /// Потребителите в базата
        /// </summary>
        private IList<User> users;

        /// <summary>
        /// Команда за вход
        /// </summary>
        private ICommand loginCommand;

        ///// <summary>
        ///// Команда при показване на екрана
        /// </summary>
        private ICommand onAppearingCommand;

        protected string Pattern => @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        /// <summary>
        /// Регекс за валидация
        /// </summary>
        private Regex regexPattern;

        #endregion

        #region Constructors

        public RegisterViewModel()
        {
            regexPattern = new Regex(Pattern);
        }

        #endregion

        #region Properties

        #region Commands

        /// <summary>
        /// Команда за вход
        /// </summary>
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new ExtendedCommand(Login));

        ///// <summary>
        ///// Команда при показване на екрана
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
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
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
        /// Въведената парола
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
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
            Username = null;
            Email = null;
            Password = null;
            ConfirmPassword = null;
        }


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

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        /// <summary>
        /// Навигира към началния екран
        /// </summary>
        private async void NavigateToMainScreen()
        {
            MessagingCenter.Send(this, "ChangedUser");
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private bool Validate()
        {
            Email = Email.Trim();
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(ConfirmPassword))
            {
                DependencyService.Resolve<IMessage>().LongAlert(AppResources.strFillData);
                return false;
            }
            if (!regexPattern.IsMatch(Email))
            {
                DependencyService.Resolve<IMessage>().LongAlert(AppResources.strInvalidEmail);
                return false;
            }
            return true;
        }

        #endregion
    }
}

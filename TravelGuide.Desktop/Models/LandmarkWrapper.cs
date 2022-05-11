using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TravelGuide.Database.Entities;
using TravelGuide.Desktop.Commands;

namespace TravelGuide.Desktop.Models
{
    public class LandmarkWrapper : INotifyPropertyChanged
    {
        #region Declarations

        private string imagePath;

        private Landmark landmark;
        private Action<LandmarkWrapper> add;
        private Action<LandmarkWrapper> cancel;
        private ICommand addCommand;
        private ICommand cancelCommand;
        private ICommand getImagePathCommand;

        #endregion

        #region Constructors

        public LandmarkWrapper(Landmark landmark, Action<LandmarkWrapper> add, Action<LandmarkWrapper> cancel)
        {
            Landmark = landmark;
            ImagePath = landmark.ImagePath;
            this.add = add;
            this.cancel = cancel;
        }

        #endregion

        #region Properties

        #region Commands

        public ICommand AddCommand => addCommand ?? (addCommand = new RelayCommand(Add, (e) => { return true; }));
        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(Cancel, (e) => { return true; }));
        public ICommand GetImagePathCommand => getImagePathCommand ?? (getImagePathCommand = new RelayCommand(GetImagePath, (e) => { return true; }));

        #endregion

        public Landmark Landmark
        {
            get => landmark;

            set => landmark = value;
        }

        public string ImagePath
        {
            get => imagePath;

            set
            {
                if(imagePath != value)
                {
                    imagePath = value;
                    Landmark.ImagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        private void GetImagePath(object e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            ImagePath = openFileDialog.FileName;
        }

        private void Add(object e)
        {
            add.Invoke(this);
        }

        private void Cancel(object e)
        {
            cancel.Invoke(this);
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}

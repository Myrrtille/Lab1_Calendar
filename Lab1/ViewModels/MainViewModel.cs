using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab1.Managers;
using Lab1.Tools;

namespace Lab1.ViewModels
{
    class MainViewModel :INotifyPropertyChanged
    {
        #region Fields

        private DateTime _date = new DateTime(1999,1,1);
        private User _user;
        private string _ageValue;
        private string _westAstrologySign;
        private string _chineseAstrologySign;

        #endregion

        #region Command + Prop

        private ICommand _confirmCommand;

        public ICommand ConfirmCommand
        {
            get { return _confirmCommand ?? (_confirmCommand = new RelayCommand<KeyEventArgs>(ConfirmExecute)); }
        }

        #endregion  

        #region Properties

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public string AgeValue
        {
            get { return _ageValue; }
            set
            {
                _ageValue = value;
                OnPropertyChanged();
            }
        }

        public string WestAstrologySign
        {
            get{return _westAstrologySign;}
            set
            {
                _westAstrologySign = value;
                OnPropertyChanged();
            }
        }

        public string ChineseAstrologySign
        {
            get { return _chineseAstrologySign; }
            set
            {
                _chineseAstrologySign = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private async void ConfirmExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                if (DateTime.Today < Date)
                {
                    MessageBox.Show("Not born yet.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Date = new DateTime(1999, 1, 1);
                    User = null;
                    WestAstrologySign = null;
                    ChineseAstrologySign = null;
                    AgeValue = null;
                }
                else
                {
                    User = new User(Date);
                    if (User.Age > 135)
                    {
                        MessageBox.Show("Too old, 135 - max age.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        Date = new DateTime(1999, 1, 1);
                        User = null;
                        WestAstrologySign = null;
                        ChineseAstrologySign = null;
                        AgeValue = null;
                    }
                }
                if (User != null)
                {
                    InitAgeValue();
                    WestAstrologySign = User.WestAstrologySign;
                    ChineseAstrologySign = User.ChineseAstrologySign;
                    if (User.Date.Month == DateTime.Today.Month && User.Date.Day == DateTime.Today.Day)
                        MessageBox.Show("Happy Birthday!!!");
                }
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
        }

        private void InitAgeValue()
        {
            if (User == null) AgeValue = "";
            if (((int)User.Age) == 0)
            {
                if (DateTime.Today.Month == Date.Month)
                {
                    AgeValue = (DateTime.Today.Day - Date.Day) + " d";
                }
                else
                {
                    DateTime d = _date;
                    int ms = 0;
                    while (d.Month != DateTime.Today.Month)
                    {
                        d = d.AddMonths(1);
                        ms++;
                    }

                    AgeValue = ms + " m";
                }
            }
            else AgeValue = (int)(User.Age) + "";
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

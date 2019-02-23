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

        private DateTime _date = new DateTime(1999, 1, 1);
        private DateTime _oldDate = new DateTime(1999, 1, 1);

        private int _age;
        private string _ageValue;

        private string _westAstrologySign;
        private string _chineseAstrologySign;

        #region Signs
        private readonly string[] _westSignsArr =
        {
            "Aquarius", //0// Jan 20 - Feb 18 
            "Pisces", //1// Feb 19 - March 20 
            "Aries", //2//Mar 21 - Apr 19   
            "Taurus", //3//Apr 20- May 20    
            "Gemini", //4//may 21 - june 20  
            "Cancer", //5//June 21 - july 22 
            "Leo", //6// july 23 - Aug 22     
            "Virgo", //7//Aug 23 - Sept 22    
            "Libra", //8// Sept 23 - Oct 22   
            "Scorpio", //9// Oct 23 - Nov 21      
            "Sagittatius", //10//Nov 22 - Dec 21  
            "Capricorn" //11// Dec 22 - Jan 19    
        };

        private readonly string[] _chineseSignArr =
        {
            "Monkey", // 2016,2004
            "Rooster", // 2017,2005,1993
            "Dog", // 2018,2006
            "Pig", // 2019,2007
            "Rat", // 2008
            "Ox", // 2009
            "Tiger", // 2010
            "Rabbit", // 2011
            "Dragon", // 2012
            "Snake", // 2013
            "Horse", // 2014
            "Sheep" // 2015
        };
        #endregion

        #endregion

        #region Command + Prop

        private ICommand _confirmCommand;

        public ICommand ConfirmCommand
        {
            get { return _confirmCommand ?? (_confirmCommand = new RelayCommand<KeyEventArgs>(ConfirmExecute)); }
        }

        #endregion  

        #region Properties

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (DateTime.Today < value)
                {
                    _oldDate = _date;
                }
                _date = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get { return _age;}
            set { _age = value; }
        }

        public string AgeValue
        {
            get { return _ageValue; }
            set
            {
                if (Age == 0)
                {
                    if (DateTime.Today.Month == Date.Month)
                    {
                        _ageValue = (DateTime.Today.Day - Date.Day) + " d";
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

                        _ageValue = ms + " m";
                    }
                }else if (Age < 0) _ageValue = "";
                else _ageValue = Age + "";
                OnPropertyChanged();
            }
        }

        public string WestAstrologySign
        {
            get { return _westAstrologySign; }
            set
            {
                _westAstrologySign = value; 
                OnPropertyChanged();
            }
        }

        public string ChineseAstrologySign
        {
            get { return _chineseAstrologySign;}
            set
            {
                if (Age < 0)
                {
                    _chineseAstrologySign = "";
                }
                else
                {
                    try
                    {
                        _chineseAstrologySign = _chineseSignArr[Date.Year % 12];
                    }
                    catch (Exception)
                    {
                        _chineseAstrologySign = "";
                    }
                }
                OnPropertyChanged();
            }
        }

        #endregion

        public MainViewModel()
        {

        }

        private async void ConfirmExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                CalculateAge();
                CalculateWestAstrology();
                ChineseAstrologySign = "";
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
        }

        private void CalculateAge()
        {
            int value = DateTime.Today.Year - _date.Year;
            if (value < 0 || (value == 0 && ((DateTime.Today.Month < Date.Month) ||
                                             (DateTime.Today.Month == Date.Month && DateTime.Today.Day < Date.Day))))
            {
                Age = -1;
                Date = _oldDate;
                
                MessageBox.Show("Not born yet.", "Error!",MessageBoxButton.OK,MessageBoxImage.Error);
                OnPropertyChanged();
                AgeValue = "";
                return;
            }
            if (value > 135)
            {
                Age = -1;
                Date = _oldDate;
                MessageBox.Show("Too old, 135 - max age.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                OnPropertyChanged();
                AgeValue = "";
                return;
            }

            else
            {
                if (DateTime.Today.Day == _date.Day && DateTime.Today.Month == _date.Month)
                    MessageBox.Show("Happy Birthday!!!");
                if ((DateTime.Today.Month < Date.Month) ||
                    (DateTime.Today.Month == Date.Month && DateTime.Today.Day < Date.Day))
                {
                    Age = value - 1;
                }
                else
                {
                    Age = value;
                }
            }
            AgeValue = "";
            OnPropertyChanged();
        }

        private void CalculateWestAstrology()
        {
            if (Age < 0)
            {
                WestAstrologySign = "";
                return;
            }
            switch (Date.Month)
            {
                case 1:
                    WestAstrologySign = Date.Day <= 20 ? _westSignsArr[11] : _westSignsArr[0];
                    return;
                case 2:
                    WestAstrologySign = Date.Day <= 18 ? _westSignsArr[0] : _westSignsArr[1];
                    return;
                case 3:
                    WestAstrologySign = Date.Day <= 20 ? _westSignsArr[1] : _westSignsArr[2];
                    return;
                case 4:
                    WestAstrologySign = Date.Day <= 20 ? _westSignsArr[2] : _westSignsArr[3];
                    return;
                case 5:
                    WestAstrologySign = Date.Day <= 20 ? _westSignsArr[3] : _westSignsArr[4];
                    return;
                case 6:
                    WestAstrologySign = Date.Day <= 21 ? _westSignsArr[4] : _westSignsArr[5];
                    return;
                case 7:
                    WestAstrologySign = Date.Day <= 22 ? _westSignsArr[5] : _westSignsArr[6];
                    return;
                case 8:
                    WestAstrologySign = Date.Day <= 22 ? _westSignsArr[6] : _westSignsArr[7];
                    return;
                case 9:
                    WestAstrologySign = Date.Day <= 22 ? _westSignsArr[7] : _westSignsArr[8];
                    return;
                case 10:
                    WestAstrologySign = Date.Day <= 23 ? _westSignsArr[8] : _westSignsArr[9];
                    return;
                case 11:
                    WestAstrologySign = Date.Day <= 22 ? _westSignsArr[9] : _westSignsArr[10];
                    return;
                case 12:
                    WestAstrologySign = Date.Day <= 21 ? _westSignsArr[10] : _westSignsArr[11];
                    return;
                default:
                    return;
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

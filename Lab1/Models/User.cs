using System;

namespace Lab1
{
    class User
    {
        #region Fields

        private DateTime _date;
        private double _age;
        private string _westAstrologySign;
        private string _chineseAstrologySign;

        #region Signs

        private readonly string[] _westSignsArr =
        {
            "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio",
            "Sagittatius", "Capricorn"
        };

        private readonly string[] _chineseSignArr =
        {
            "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Sheep"
        };
        #endregion

        #endregion

        #region Properties

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public double Age
        {
            get
            {
                _age = (DateTime.Today.Subtract(Date)).TotalDays / 365;
                return _age;
            }
            private set { _age = value; }
        }

        public string WestAstrologySign
        {
            get
            {
                switch (Date.Month)
                {
                    case 1:
                        return Date.Day <= 20 ? _westSignsArr[11] : _westSignsArr[0];
                    case 2:
                        return Date.Day <= 18 ? _westSignsArr[0] : _westSignsArr[1];
                    case 3:
                        return Date.Day <= 20 ? _westSignsArr[1] : _westSignsArr[2];
                    case 4:
                        return Date.Day <= 20 ? _westSignsArr[2] : _westSignsArr[3];
                    case 5:
                        return Date.Day <= 20 ? _westSignsArr[3] : _westSignsArr[4];
                    case 6:
                        return Date.Day <= 21 ? _westSignsArr[4] : _westSignsArr[5];
                    case 7:
                        return Date.Day <= 22 ? _westSignsArr[5] : _westSignsArr[6];
                    case 8:
                        return Date.Day <= 22 ? _westSignsArr[6] : _westSignsArr[7];
                    case 9:
                        return Date.Day <= 22 ? _westSignsArr[7] : _westSignsArr[8];
                    case 10:
                        return Date.Day <= 23 ? _westSignsArr[8] : _westSignsArr[9];
                    case 11:
                        return Date.Day <= 22 ? _westSignsArr[9] : _westSignsArr[10];
                    case 12:
                        return Date.Day <= 21 ? _westSignsArr[10] : _westSignsArr[11];
                    default:
                        return "";
                }
            }
            private set { _westAstrologySign = value; }
        }

        public string ChineseAstrologySign
        {
            get
            {
                if (Age < 0)
                    return "";
                else
                {
                    try
                    {
                        return _chineseSignArr[Date.Year % 12];
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                }
            }
            private set { _chineseAstrologySign = value; }
        }

        #endregion

        #region Constructor

        public User(DateTime date)
        {
            _date = date;
        }

        #endregion

    }
}

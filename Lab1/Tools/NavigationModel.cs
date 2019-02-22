using Lab1.Views;
using System;

namespace Lab1.Tools
{
    internal enum ModesEnum
    {
        Main
    }
    class NavigationModel
    {
        private readonly IContentWindow _contentWindow;
        private MainView _mainView;

        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = _mainView ?? (_mainView = new MainView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}

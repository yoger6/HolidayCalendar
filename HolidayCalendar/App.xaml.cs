using System;
using System.Windows;
using HolidayCalendar.View;
using HolidayCalendar.ViewModel;

namespace HolidayCalendar
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SetupMainView();
            base.OnStartup(e);
        }
        
        private void SetupMainView()
        {
            var mainView = new MainView();
            var mainViewModel = new MainViewModel();
            mainView.DataContext = mainViewModel;
            Exit += (sender, args) => mainViewModel.Close();

            mainView.Show();
        }
    }
}

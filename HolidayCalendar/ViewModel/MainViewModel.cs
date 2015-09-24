using DataLib.Repositories;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    public class MainViewModel : ViewModel
    {
        private ChildViewModel _currentViewModel;
        
        public ChildViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (Equals(value, _currentViewModel)) return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public string Title => "Holiday Calendar";


        public MainViewModel()
        {
            LoadInitialViewModel();
        }


        private void LoadInitialViewModel()
        {
            var loginViewModel = new LoginViewModel(new DirectoryAuthenticatorDummy(), new EmployeeRepository());
            loginViewModel.MainViewModel = this;
            CurrentViewModel = loginViewModel;
        }

        public void LoadModel(ChildViewModel viewModel)
        {
            UnloadCurrentViewModel();
            viewModel.MainViewModel = this;
            CurrentViewModel = viewModel;
            viewModel.OnLoaded();
        }

        private void UnloadCurrentViewModel()
        {
            if (CurrentViewModel != null)
            {
                CurrentViewModel.OnUnloaded();
                CurrentViewModel = null;
            }
        }

        public void Close()
        {
            UnloadCurrentViewModel();
        }
    }
}

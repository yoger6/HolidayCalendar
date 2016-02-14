using DataLib.Repositories;
using HolidayCalendar.Tools;

namespace HolidayCalendar.ViewModel
{
    public class MainViewModel : ViewModel
    {
        private ChildViewModel _currentViewModel;
        private UtilityViewModel _currentUtilityViewModel;

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
        public UtilityViewModel CurrentUtilityViewModel
        {
            get { return _currentUtilityViewModel; }
            set
            {
                if (Equals(value, _currentUtilityViewModel)) return;
                _currentUtilityViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsUtilityPresent));
            }
        }
        public string Title => "Holiday Calendar";
        public bool IsUtilityPresent => _currentUtilityViewModel != null;
        

        public MainViewModel()
        {
            LoadInitialViewModel();
        }


        private void LoadInitialViewModel()
        {
            var connectionString = SettingsHelper.GetConnectionString();

            if (ConnectionVerifier.IsConnectionValid(connectionString))
            {
                LoadUtilityViewModel(new LoginViewModel(this));
            }
            else
            {
                CurrentUtilityViewModel = new DatabaseLoginViewModel(this);
            }
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

        public void UnloadUtilityViewModel()
        {
            CurrentUtilityViewModel = null;
        }

        public void LoadUtilityViewModel(UtilityViewModel viewModel)
        {
            CurrentUtilityViewModel = viewModel;
        }
    }
}

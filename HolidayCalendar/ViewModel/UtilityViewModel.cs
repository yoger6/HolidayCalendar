using System;

namespace HolidayCalendar.ViewModel
{
    public abstract class UtilityViewModel : ViewModel
    {
        protected readonly MainViewModel MainViewModel;

        private string _title;
        private string _errorMessage;

        public string Title
        {
            get { return _title; }
            protected set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasErrors));
            }
        }
        public bool HasErrors => !string.IsNullOrWhiteSpace(_errorMessage);

        protected UtilityViewModel(MainViewModel mainViewModel)
        {
            if (mainViewModel == null)
            {
                throw new ArgumentNullException("mainViewModel cannot be null. ");
            }
            MainViewModel = mainViewModel;
        }


        protected virtual void AppendErrorMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                ErrorMessage += ", ";
            }

            ErrorMessage += message;
        }

        protected virtual void AppendErrorMessageEnd()
        {
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                ErrorMessage += " is required";
            }
        }

        protected virtual void Close()
        {
            MainViewModel.UnloadUtilityViewModel();
        }

        protected void LoadViewModel(ChildViewModel viewModel)
        {
            MainViewModel.LoadModel(viewModel);
        }

        protected void ChangeUtility(UtilityViewModel viewModel)
        {
            MainViewModel.LoadUtilityViewModel(viewModel);
        }
    }
}
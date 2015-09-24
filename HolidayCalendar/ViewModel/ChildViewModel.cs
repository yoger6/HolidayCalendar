using System;

namespace HolidayCalendar.ViewModel
{
    public abstract class ChildViewModel : ViewModel
    {

        public MainViewModel MainViewModel;
        
        public void NavigateTo(ChildViewModel viewModel)
        {
            if (MainViewModel == null)
            {
                throw new NullReferenceException("ChildViewModel must have MainViewModel defined, " +
                                                 "it's done by default when using MainViewModel to load it. " +
                                                 "Consider doing this or assign MainViewModel manually.");
            }
            MainViewModel.LoadModel(viewModel);
        }

        public virtual void OnLoaded() { }
        public virtual void OnUnloaded() { }
    }
}
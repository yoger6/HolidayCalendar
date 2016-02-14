using System.Collections.Generic;
using DataLib.Model;
using DataLib.Repositories;

namespace HolidayCalendar.ViewModel
{
    public class LegendViewModel : ViewModel
    {
        private readonly IHolidayReasonRepository _holidayReasonRepository;

        public string Title => "Color Key";
        public IList<HolidayReason> Reasons { get; private set; }

        public LegendViewModel() 
        : this(new HolidayReasonRepository(SettingsHelper.GetConnectionString()))
        {
        }

        public LegendViewModel(IHolidayReasonRepository repository)
        {
            _holidayReasonRepository = repository;
            FillLegendItems();
        }

        private void FillLegendItems()
        {
            using (_holidayReasonRepository)
            {
                Reasons = _holidayReasonRepository.GetAll();
            }
        }
    }
}

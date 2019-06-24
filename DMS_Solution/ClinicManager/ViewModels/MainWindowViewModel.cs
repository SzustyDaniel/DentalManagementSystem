using ClinicManager.Services;
using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IManagementApiService _managementApiService;

        private ObservableCollection<DailyEmployeeReport> _dailyEmployeeReports;
        //public ObservableCollection<DailyEmployeeReport> DailyEmployeeReports
        //{
        //    get { return _dailyEmployeeReports; }
        //    set { SetProperty(ref _dailyEmployeeReports, value); }
        //}

        private DateTime _pickedDate;
        //public DateTime PickedDate
        //{
        //    get => _pickedDate;
        //    set
        //    {
        //        if (value == _pickedDate) return;
        //        _pickedDate = value;
        //        ReportMatchesPickedDate = false;
        //    }
        //}

        private bool _reportMatchesPickedDate;
        //public bool ReportMatchesPickedDate
        //{
        //    get { return _reportMatchesPickedDate; }
        //    set { SetProperty(ref _reportMatchesPickedDate, value); }
        //}

        //private DelegateCommand _showReportCommand;
        //public DelegateCommand ShowReportCommand =>
        //    _showReportCommand ?? (_showReportCommand = new DelegateCommand(ExecuteShowReportCommandAsync)).
        //    ObservesCanExecute(() => !ReportMatchesPickedDate || PickedDate == default);
        //private async void ExecuteShowReportCommandAsync()
        //{
        //    ReportMatchesPickedDate = true;
        //    IEnumerable<DailyEmployeeReport> reports = await _managementApiService.GetDailyEmployeeReports(PickedDate);
        //    DailyEmployeeReports = new ObservableCollection<DailyEmployeeReport>(reports);
        //}

        public MainWindowViewModel(IManagementApiService managementApiService)
        {
            _managementApiService = managementApiService;
        }

        
    }
}

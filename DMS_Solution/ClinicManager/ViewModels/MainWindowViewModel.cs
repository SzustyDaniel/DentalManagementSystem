using ClinicManager.Services;
using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ClinicManager.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IManagementApiService _managementApiService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<DailyEmployeeReport> _dailyEmployeeReports;
        public ObservableCollection<DailyEmployeeReport> DailyEmployeeReports
        {
            get => _dailyEmployeeReports;
            set
            {
                if (_dailyEmployeeReports == value) return;
                _dailyEmployeeReports = value;
                OnPropertyChanged();
            }
        }

        public DateTime? PickedDate { get; set; }

        public bool IsBusy { get; private set; }

        public ICommand ShowReportCommand { get; }
        private async void ExecuteShowReportCommand(object parameter)
        {
            try
            {
                IsBusy = true;
                IEnumerable<DailyEmployeeReport> reports = await _managementApiService.GetDailyEmployeeReports(PickedDate.Value);
                DailyEmployeeReports = new ObservableCollection<DailyEmployeeReport>(reports);
            }
            catch (Exception)
            {
                _dialogService.ShowMessageBox(
                    "There was a problem retrieving the report from server. Please try again", "Clinic Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private bool CanExecuteShowReportCommand(object parameter)
        {
            return !IsBusy && PickedDate != default;
        }

        public MainWindowViewModel(IManagementApiService managementApiService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _managementApiService = managementApiService;
            ShowReportCommand = new CustomCommand(CanExecuteShowReportCommand, ExecuteShowReportCommand);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

﻿using ClinicManager.Services;
using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ClinicManager.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IManagementApiService _managementApiService;

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

        private bool _isBusy;

        public ICommand ShowReportCommand { get; }
        private async void ExecuteShowReportCommand(object parameter)
        {
            _isBusy = true;
            IEnumerable<DailyEmployeeReport> reports = await _managementApiService.GetDailyEmployeeReports(PickedDate.Value);
            DailyEmployeeReports = new ObservableCollection<DailyEmployeeReport>(reports);
            _isBusy = false;
        }
        private bool CanExecuteShowReportCommand(object parameter)
        {
            return !_isBusy && PickedDate != default;
        }

        public MainWindowViewModel(IManagementApiService managementApiService)
        {
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
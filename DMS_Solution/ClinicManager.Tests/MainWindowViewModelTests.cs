using ClinicManager.Services;
using ClinicManager.ViewModels;
using Common.UserModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClinicManager.Tests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void ShowReportCommand_CannotExecute_WhenDidNotPickDate()
        {
            MainWindowViewModel model = new MainWindowViewModel(new ManagementApiServiceMock(), new DialogServiceMock())
            {
                PickedDate = null
            };

            bool canExecute = model.ShowReportCommand.CanExecute(null);
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void DailyEmployeeReports_PickDateAndExecuteShowReportCommand_DailyEmployeeReportsNotEmpty()
        {
            MainWindowViewModel model = new MainWindowViewModel(new ManagementApiServiceMock(), new DialogServiceMock())
            {
                PickedDate = DateTime.Today
            };

            model.ShowReportCommand.Execute(null);

            bool dailyReportsNotEmpty = model.DailyEmployeeReports.Any();
            Assert.IsTrue(dailyReportsNotEmpty);
        }
    }

    public class ManagementApiServiceMock : IManagementApiService
    {
        public Task<IList<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date)
        {
            IList<DailyEmployeeReport> list = new List<DailyEmployeeReport>
            {
                new DailyEmployeeReport()
            };
            return Task.FromResult(list);
        }
    }

    public class DialogServiceMock : IDialogService
    {
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBoxResult.OK;
        }
    }
}

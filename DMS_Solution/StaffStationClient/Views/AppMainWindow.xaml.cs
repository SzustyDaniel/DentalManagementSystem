using Prism.Events;
using StaffStationClient.Services;
using StaffStationClient.Utility;
using System.Windows;

namespace StaffStationClient.Views
{
    /// <summary>
    /// Interaction logic for AppMainWindow.xaml
    /// </summary>
    public partial class AppMainWindow : Window
    {
        public AppMainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IEventAggregator eventAggregator = ApplicationService.Instance.EventAggregator;
            eventAggregator.GetEvent<LogUserForceEvent>().Publish();
        }
    }
}

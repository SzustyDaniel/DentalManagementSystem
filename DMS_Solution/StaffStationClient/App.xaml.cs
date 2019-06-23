using StaffStationClient.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Prism.Mvvm;
using StaffStationClient.ViewModels;
using StaffStationClient.Services;

namespace StaffStationClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<AppMainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register(typeof(AppMainWindow).ToString(),() => new AppMainWindowViewModel(ApplicationService.Instance.EventAggregator,HttpActionsService.Instance,DialogService.Instance));
            ViewModelLocationProvider.Register(typeof(LoginUC).ToString(), () => new LoginUCViewModel(ApplicationService.Instance.EventAggregator, HttpActionsService.Instance, DialogService.Instance));
            ViewModelLocationProvider.Register(typeof(StaffControlUC).ToString(), () => new StaffControlUCViewModel(ApplicationService.Instance.EventAggregator, HttpActionsService.Instance, DialogService.Instance));

        }
    }
}

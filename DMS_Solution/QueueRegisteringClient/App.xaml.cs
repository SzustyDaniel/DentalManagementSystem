using QueueRegisteringClient.Views;
using Prism.Ioc;
using System.Windows;
using Prism.Mvvm;
using QueueRegisteringClient.ViewModels;
using QueueRegisteringClient.Services;

namespace QueueRegisteringClient
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

            ViewModelLocationProvider.Register(typeof(WelcomeComponent).ToString(), () => new WelcomeComponentViewModel(ApplicationService.Instance.EventAggregator, ClientHttpActions.Instance, ViewsDialog.Instance));
            ViewModelLocationProvider.Register(typeof(AppMainWindow).ToString(), () => new AppMainWindowViewModel(ApplicationService.Instance.EventAggregator,ViewsDialog.Instance));
            ViewModelLocationProvider.Register(typeof(SelectQueueComponent).ToString(), () => new SelectQueueComponentViewModel(ApplicationService.Instance.EventAggregator, ClientHttpActions.Instance, ViewsDialog.Instance));
            ViewModelLocationProvider.Register(typeof(QueueDetailsDisplayComponent).ToString(), () => new QueueDetailsDisplayComponentViewModel(ApplicationService.Instance.EventAggregator));
        }

    }
}

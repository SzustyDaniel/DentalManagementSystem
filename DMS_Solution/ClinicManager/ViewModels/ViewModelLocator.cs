using ClinicManager.Services;

namespace ClinicManager.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel { get; } =
            new MainWindowViewModel(new ManagementApiService(), new DialogService());
    }
}

using ClinicManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.ViewModels
{
    public class ViewModelLocator
    {
        public static MainWindowViewModel MainWindowViewModel { get; } =
            new MainWindowViewModel(new ManagementApiService());
    }
}

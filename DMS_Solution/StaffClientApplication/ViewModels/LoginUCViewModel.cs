using Prism.Commands;
using Prism.Mvvm;
using StaffClientApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaffClientApplication.ViewModels
{
    public class LoginUCViewModel : BindableBase
    {
        private StationModel model;
        public StationModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }


        public LoginUCViewModel()
        {

        }
    }
}

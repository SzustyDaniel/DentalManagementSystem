using Prism.Commands;
using Prism.Mvvm;
using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaffStationClient.ViewModels
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

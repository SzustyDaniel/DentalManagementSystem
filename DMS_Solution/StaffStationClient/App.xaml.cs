﻿using StaffStationClient.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

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
    }
}
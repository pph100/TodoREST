﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            var buildVersion = Assembly.GetAssembly(typeof(TodoREST.AboutPage)).GetName().Version;
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * buildVersion.Build + TimeSpan.TicksPerSecond * 2 * buildVersion.Revision));

            InitializeComponent();

            if (Device.RuntimePlatform != Device.macOS)
            {
                var appName = AppInfo.Name;
                var packageName = AppInfo.PackageName;
                var version = AppInfo.VersionString;
                var build = AppInfo.BuildString;

                build += " (built: " + buildDateTime.ToString() + ")";
                xAppInfo.Text += appName;
                xPackageName.Text += packageName;
                xVersion.Text += version;
                xBuild.Text += build;
            }
            else
            {
                // Application Name
                var appName = "Todo REST Crypto";

                // Package Name/Application Identifier (com.microsoft.testapp)
                var packageName = "Crypto App for macOS";

                // Application Version (1.0.0)
                var version = "x";

                // Application Build Number (1)
                var build = "built: " + buildDateTime.ToString();

                xAppInfo.Text += appName;
                xPackageName.Text += packageName;
                xVersion.Text += version;
                xBuild.Text += build;
            }

        }
    }
}

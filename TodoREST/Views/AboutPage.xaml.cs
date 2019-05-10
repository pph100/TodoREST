using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TodoREST
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            // Application Name
            var appName = AppInfo.Name;

            // Package Name/Application Identifier (com.microsoft.testapp)
            var packageName = AppInfo.PackageName;

            // Application Version (1.0.0)
            var version = AppInfo.VersionString;

            // Application Build Number (1)
            var build = AppInfo.BuildString;

            xAppInfo.Text += appName;
            xPackageName.Text += packageName;
            xVersion.Text += version;
            xBuild.Text += build;
        }
    }
}

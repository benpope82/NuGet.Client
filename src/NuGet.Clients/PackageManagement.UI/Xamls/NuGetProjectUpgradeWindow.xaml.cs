﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace NuGet.PackageManagement.UI
{
    /// <summary>
    /// Interaction logic for NuGetProjectUpgradeWindow.xaml
    /// </summary>
    public partial class NuGetProjectUpgradeWindow : VsDialogWindow
    {
        public NuGetProjectUpgradeWindow(NuGetProjectUpgradeWindowModel model)
        {
            DataContext = model;
            InitializeComponent();
        }

        private void OkButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ExecuteOpenExternalLink(object sender, ExecutedRoutedEventArgs e)
        {
            var hyperlink = (Hyperlink) e.OriginalSource;
            if (hyperlink.NavigateUri != null)
            {
                Process.Start(hyperlink.NavigateUri.AbsoluteUri);
                e.Handled = true;
            }
        }
    }
}

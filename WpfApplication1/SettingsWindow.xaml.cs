using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Configuration;

namespace TVShowRenamer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        
        public SettingsWindow()
        {
            InitializeComponent();

        }

        private void checkboxTitleCase_Checked(object sender, RoutedEventArgs e)
        {
            //ConfigurationManager.AppSettings["lowercaseWords"] = "true";

            new TVShowRenamer.settingsChanger().changeUserSettings("lowercaseWords", "true");

        }

        private void checkboxDefaultLib_Checked(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.settingsChanger().changeUserSettings("useDefaultLibraryDir", "true");
        }

        private void checkboxTitleCase_Unchecked(object sender, RoutedEventArgs e)
        {

            new TVShowRenamer.settingsChanger().changeUserSettings("lowercaseWords", "false");
        }

        private void checkboxDefaultLib_Unchecked(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.settingsChanger().changeUserSettings("useDefaultLibraryDir", "false");
            new TVShowRenamer.settingsChanger().changeUserSettings("moveToShowFolder", "false");
            checkboxMoveToShowFolder.IsChecked = false;

        }

        private void btnBrowseLibDir_Click(object sender, RoutedEventArgs e)
        {
            textblockLibDir.Text = new TVShowRenamer.BrowseDirectory().BrowseDir();
            new TVShowRenamer.settingsChanger().changeUserSettings("defaultLibraryDir", textblockLibDir.Text);

        }

        private void btnBrowseDefaultDir_Click(object sender, RoutedEventArgs e)
        {
            textblockDefaultDir.Text = new TVShowRenamer.BrowseDirectory().BrowseDir();
            new TVShowRenamer.settingsChanger().changeUserSettings("defaultBrowseDir", textblockDefaultDir.Text);
        }

        private void textblockLibDir_Loaded(object sender, RoutedEventArgs e)
        {
            textblockLibDir.Text = ConfigurationManager.AppSettings["defaultLibraryDir"];


        }

        private void textblockDefaultDir_Loaded(object sender, RoutedEventArgs e)
        {
            textblockDefaultDir.Text = ConfigurationManager.AppSettings["defaultBrowseDir"];
        }

        private void textboxChooseSyntax_Loaded(object sender, RoutedEventArgs e)
        {
            textboxChooseSyntax.Text = ConfigurationManager.AppSettings["defaultSyntax"];
        }

        private void textboxChooseSyntax_TextChanged(object sender, TextChangedEventArgs e)
        {
             new TVShowRenamer.settingsChanger().changeUserSettings("defaultSyntax", textboxChooseSyntax.Text);
        }

        private void checkboxTitleCase_Loaded(object sender, RoutedEventArgs e)
        {
            if(ConfigurationManager.AppSettings["lowercaseWords"] == "true")
            {
                checkboxTitleCase.IsChecked = true;

            }
            else
            {
                checkboxTitleCase.IsChecked = false;

            }
        }

        private void checkboxDefaultLib_Loaded(object sender, RoutedEventArgs e)
        {
            if(ConfigurationManager.AppSettings["useDefaultLibraryDir"] == "true")
            { checkboxDefaultLib.IsChecked = true; }
            else { checkboxDefaultLib.IsChecked = false; }
        }

        private void checkboxLightTheme_Loaded(object sender, RoutedEventArgs e)
        {
            if(ConfigurationManager.AppSettings["useLightTheme"] == "true")
            {
                checkboxLightTheme.IsChecked = true;
                MahApps.Metro.ThemeManager.ChangeAppTheme(Application.Current, "BaseLight");
            }
            else
            {
                checkboxLightTheme.IsChecked = false;
                MahApps.Metro.ThemeManager.ChangeAppTheme(Application.Current, "BaseDark");
            }
        }

        private void checkboxLightTheme_Checked(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.settingsChanger().changeUserSettings("useLightTheme", "true");
            MahApps.Metro.ThemeManager.ChangeAppTheme(Application.Current, "BaseLight");
        }

        private void checkboxLightTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.settingsChanger().changeUserSettings("useLightTheme", "false");
            MahApps.Metro.ThemeManager.ChangeAppTheme(Application.Current, "BaseDark");
        }

        private void checkboxMoveToShowFolder_Loaded(object sender, RoutedEventArgs e)
        {
            if (ConfigurationManager.AppSettings["moveToShowFolder"] == "true")
            {
                checkboxMoveToShowFolder.IsChecked = true;

            }
            else
            {
                checkboxMoveToShowFolder.IsChecked = false;

            }
        }

        private void checkboxMoveToShowFolder_Checked(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.settingsChanger().changeUserSettings("moveToShowFolder", "true");
            new TVShowRenamer.settingsChanger().changeUserSettings("useDefaultLibraryDir", "true");
            checkboxDefaultLib.IsChecked = true;
            
        }

        private void checkboxMoveToShowFolder_Unchecked(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.settingsChanger().changeUserSettings("moveToShowFolder", "false");
            
        }
    }
}

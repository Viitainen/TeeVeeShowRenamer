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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Configuration;

namespace TVShowRenamer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        

        public MainWindow()
        {
            InitializeComponent();

            if (ConfigurationManager.AppSettings["useLightTheme"] == "true")
            { MahApps.Metro.ThemeManager.ChangeAppTheme(Application.Current, "BaseLight"); }
            else
            { MahApps.Metro.ThemeManager.ChangeAppTheme(Application.Current, "BaseDark");}
       
        }


        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
           
            new TVShowRenamer.NameRenamer().actualRenaming();
            datagridFiles.Items.Refresh();
            datagridNewFiles.Items.Refresh();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            BrowsingFiles.BrowseFiles();
            
        }

        private void btnClearList_Click(object sender, RoutedEventArgs e)
        {
            BrowsingFiles.ClearList();
            
            
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {

            datagridFiles.ItemsSource = BrowsingFiles.ReturnList();
        }

        private void btnDeleteFromList_Click(object sender, RoutedEventArgs e)
        {
            while (datagridFiles.SelectedItems.Count > 0)
            {
                BrowsingFiles.DeleteFromList((listFile) datagridFiles.SelectedItem);
                
            }
        }


        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            new TVShowRenamer.NameRenamer().RenameCurrentNames(textboxSyntax.Text);
            datagridNewFiles.ItemsSource = BrowsingFiles.ReturnList();
        }

        private void bntRefresh_Click(object sender, RoutedEventArgs e)
        {
            datagridFiles.Items.Refresh();
            datagridNewFiles.Items.Refresh();
        }

        private void textboxSyntax_Loaded(object sender, RoutedEventArgs e)
        {
            
            textboxSyntax.Text = ConfigurationManager.AppSettings["defaultSyntax"];

            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Owner = Application.Current.MainWindow;
            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settingsWindow.ShowDialog();
        }
        

    }
}

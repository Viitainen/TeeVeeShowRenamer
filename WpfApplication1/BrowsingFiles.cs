using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Configuration;

namespace TVShowRenamer
{
   static class BrowsingFiles
    {

       static ObservableCollection<listFile> fileNames2 {get;set;}

       static BrowsingFiles()
       {

           fileNames2 = new ObservableCollection<listFile>();
 
       }



        public static void BrowseFiles()
        {

            
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            dlg.Multiselect = true;
            dlg.FileName = "Subtitle and video files";
            dlg.DefaultExt = ".srt";
            dlg.InitialDirectory = ConfigurationManager.AppSettings["defaultBrowseDir"];
            dlg.Filter = "Subtitles and videofiles (*.srt, *.sub, *.mkv, *.avi, *.mp4) |*.srt; *.sub; *.mkv; *.avi; *.mp4; | All files (*.*) | *.*";



            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
               foreach(String f in dlg.FileNames)
               {
                   
                   try
                   {

                       fileNames2.Add(new listFile(Path.GetDirectoryName(f), Path.GetFileNameWithoutExtension(f), Path.GetExtension(f),"",""));
                      
                       
                   }

                   catch(SystemException ex)
                   {
                       MessageBox.Show("Cannot get names from files: " + f.Substring(f.LastIndexOf('\\')) + "Reported error: " + ex.Message);


                   }
                   

               }

               

            }
            
        }


        public static ObservableCollection<listFile> ReturnList()
        {
            return fileNames2;
        }

        public static void ClearList()
        {
            fileNames2.Clear();

        }


       public static void DeleteFromList(listFile item)
        {
           fileNames2.Remove(item);

        }
       
       

        }



    }

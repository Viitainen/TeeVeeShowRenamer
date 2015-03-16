using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Configuration;

namespace TVShowRenamer
{
    
   public class NameRenamer
    {

       private string lowercaseWords;
       private string useDefaultLibDir;
       private string defaultLibDir;
       private string moveToShowFolder;

       public NameRenamer() {
           lowercaseWords = ConfigurationManager.AppSettings["lowercaseWords"];
           useDefaultLibDir = ConfigurationManager.AppSettings["useDefaultLibraryDir"];
           defaultLibDir = ConfigurationManager.AppSettings["defaultLibraryDir"];
           moveToShowFolder = ConfigurationManager.AppSettings["moveToShowFolder"];
           
        }
       public ObservableCollection<listFile> ReturnCurrentList()
       {
           return BrowsingFiles.ReturnList();
           
       }

       public ObservableCollection<listFile> ReturnNewList()
       {
           return BrowsingFiles.ReturnList();

       }

       public void RenameCurrentNames(string renameSyntax)
       {
           //loop all files
           string renamingSyntax = renameSyntax;

           foreach (listFile cFile in BrowsingFiles.ReturnList())
           {
               //Inits
               cFile.newFileName = cFile.fileName;
               cFile.newFilePath = cFile.filePath;
               string showsName = "";
               int lastWord = 0;
               int i = 0;
               string seasonNumber = "";
               string episodeNumber = "";
               bool foundEpisodeNumbers = false;
               string[] splittedName = cFile.newFileName.Split(new Char[] {'.',' '});
               //---------------------------------------------

               //each string in splittedName array
               foreach(string s in splittedName) 
               {
                   System.Diagnostics.Debug.WriteLine("Substring " + i + ": " + s);


                                        
                                       if(s.Length == 6) //if syntax is Helix S02E05
                       
				                        {
                                                if (char.ToLower(s[0]) == 's' && char.IsNumber(s[1]) && char.IsNumber(s[2]) && (char.ToLower(s[3]) == 'e') && char.IsNumber(s[4]) && char.IsNumber(s[5]))
				   	                                {
				   		                                lastWord = i;
				   		                                seasonNumber = s[1].ToString() + s[2].ToString();
				   		                                episodeNumber = s[4].ToString() +s[5].ToString();
                                                        foundEpisodeNumbers = true;
				   		                                break;

				   	                                }
                                       }

                                       else if (s.Length == 5) //if syntax is Helix 02x05
                                       {
                                                if(char.IsNumber(s[0]) && char.IsNumber(s[1])  && char.ToLower(s[2]) == 'x'  && char.IsNumber(s[3]) && char.IsNumber(s[4]))
				   	                            {
				   		                            lastWord = i;
				   		                            seasonNumber = s[0].ToString() + s[1].ToString() ;
				   		                            episodeNumber = s[3].ToString() + s[4].ToString() ;
                                                    foundEpisodeNumbers = true;
				   		                            break;
				   	                            }

                                      }


                                       else if (s.Length == 4) //if syntax is Helix 2x05
                                       {
                                           if (char.IsNumber(s[0]) && char.ToLower(s[1]) == 'x' && char.IsNumber(s[2]) && char.IsNumber(s[3]))
                                           {
                                               lastWord = i;
                                               seasonNumber = s[0].ToString();
                                               episodeNumber = s[2].ToString() + s[3].ToString();
                                               foundEpisodeNumbers = true;
                                               break;
                                           }

                                       }




                                       else if (s.Length == 3 && char.IsNumber(s[0]) && char.IsNumber(s[1]) && char.IsNumber(s[2])) //if syntax is Helix 205
                                      {
                                               lastWord = i;
                                               seasonNumber = "0" + s[0].ToString();
                                               episodeNumber = s[1].ToString() +s[2].ToString();
                                               foundEpisodeNumbers = true;
                                               break;

                                      }


                                       else if (s.Length == 3) //if syntax is Helix 2x5
                                       {
                                           if (char.IsNumber(s[0]) && char.ToLower(s[1]) == 'x' && char.IsNumber(s[3]))
                                           {
                                               lastWord = i;
                                               seasonNumber = s[0].ToString();
                                               episodeNumber = s[2].ToString();
                                               foundEpisodeNumbers = true;
                                               break;
                                           }

                                       }

                     i++;

               }

               //Uppercasing the strings

               for (int l = 0; l < splittedName.Length; l++)
               {
                    if(l==0)
                    {
                        splittedName[l] = stringFirstCaseUpperForReal(splittedName[l]);

                    }
                    else
                    {
                        splittedName[l] = stringFirstCaseUpper(splittedName[l]);

                        if (splittedName[l] == "-")
                        {
                            splittedName[l] = "";
                        }
                   

                    }




               }





               //Different renaming regime if episodenumbering was "found"
               if(!foundEpisodeNumbers)
               {
                   lastWord = splittedName.Length;
               }
                       for (int j = lastWord; j < splittedName.Length; j++)
                       {

                                         splittedName[j] = "";

                       }

                        //If string isn't empty, add it to the end of the shows name
                       for (int k = 0; k < splittedName.Length; k++)
                       {

                               if (!string.IsNullOrEmpty(splittedName[k]))
                               {

                                   showsName += splittedName[k].ToString() + " ";

                               }


                       }
                        
                       //Trim the extra space
                       showsName = showsName.Trim();

                            //if episodenumbers were found use this syntax
                           if (foundEpisodeNumbers)
                           {
                               string tempName;
                               tempName = renamingSyntax.Replace("[showname]", showsName).Replace("[eNum]",episodeNumber).Replace("[sNum]",seasonNumber);

                               cFile.newFileName = tempName;

                               

                           }
                           else
                           {
                               cFile.newFileName = showsName;

                           }


                            
                           if (useDefaultLibDir == "true")
                           {
                               System.Diagnostics.Debug.WriteLine("UsedefaultLibDir = " + useDefaultLibDir);
                                        if(moveToShowFolder == "true")
                                        {
                                            System.Diagnostics.Debug.WriteLine("defaultLibdir + \\ showsname = " + defaultLibDir + "\\" + showsName);
                                                    if(Directory.Exists(defaultLibDir + "\\" + showsName))
                                                    {
                                                        cFile.newFilePath = defaultLibDir + "\\" + showsName;

                                                    }
                                                    else
                                                    {
                                                        cFile.newFilePath = defaultLibDir;
                                                    }

                                        }
                                        else
                                        {
                                            cFile.newFilePath = defaultLibDir;
                                        }
                               
                           }


           }

           //BrowsingFiles.SetNewFileNames(newFiles);
       }



       private string stringFirstCaseUpper(string theString)
       {
           string[] excludedWords = {"to" ,"and","a","but","or","on","in","with","from","an","the"};

           System.Diagnostics.Debug.WriteLine("lowercaseWords == " + lowercaseWords);
           if(lowercaseWords == "true")
           {
               if(!string.IsNullOrEmpty(theString))
               {
                           if(!excludedWords.Contains(theString.ToLower()))
                           {
                               System.Diagnostics.Debug.WriteLine(theString + " kirjoitetaan alkamaan isolla alkukirjamiella");
                               char[] a = theString.ToCharArray();
                               a[0] = char.ToUpper(a[0]);
                               return new string(a);

                           }
                           else
                           {
                               System.Diagnostics.Debug.WriteLine(theString + " kirjoitetaan alkamaan PIENELLÄ");
                               char[] a = theString.ToCharArray();
                               a[0] = char.ToLower(a[0]);
                               return new string(a);

                             }

                }

               else
               {
                   return theString;

               }

           }

          else
          {
               char[] a = theString.ToCharArray();
               a[0] = char.ToUpper(a[0]);
               return new string(a);

          }

       }

       private string stringFirstCaseUpperForReal(string stringen)
       {
           char[] c = stringen.ToCharArray();
           c[0] = char.ToUpper(c[0]);
           return new string(c);

       }




       public void actualRenaming()
       {
           int filesRenamed = 0;
           foreach(listFile cFile in BrowsingFiles.ReturnList())
           {
               try
               {
                   
                   if (File.Exists(cFile.filePath + "\\" + cFile.fileName + cFile.fileExt))
                   {
                       System.Diagnostics.Debug.WriteLine("Moving file: " + cFile.filePath + "\\" + cFile.fileName + cFile.fileExt + " New file: " + cFile.newFilePath + "\\" + cFile.newFileName + cFile.fileExt);
                       File.Move((cFile.filePath + "\\" + cFile.fileName + cFile.fileExt), (cFile.newFilePath + "\\" + cFile.newFileName + cFile.fileExt));
                       cFile.filePath = cFile.newFilePath;
                       cFile.fileName = cFile.newFileName;

                       filesRenamed++;
                   }
                   


               }
               catch (SystemException ex)
               {

                       MessageBox.Show("Cannot rename " + cFile.fileName + "" + ex.Message);

                       if (MessageBox.Show("Overwrite", "Question", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                       {

                           File.Copy((cFile.filePath + "\\" + cFile.fileName + cFile.fileExt), (cFile.newFilePath + "\\" + cFile.newFileName + cFile.fileExt),true);
                           File.Delete(cFile.filePath + "\\" + cFile.fileName + cFile.fileExt);
                           cFile.filePath = cFile.newFilePath;
                           cFile.fileName = cFile.newFileName;
                           filesRenamed++;
                       }

                       
               }
              
           }
           MessageBox.Show(filesRenamed + " files renamed.");
           

       }
    }
}

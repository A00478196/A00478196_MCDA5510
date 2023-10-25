using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ProgAssign1
{
    public class DirWalker
    {
        List<string> files = new List<string>();
        public List<string> Walker()
        {

            // path to read the sample data
            //Console.Write("Enter the full path for directory of sample data. For example: C://Assingment1/Sample data// :");
            //string path = Console.ReadLine();
            string path = "D:\\Sample_Data\\";


            // try catch block to catch any file/directory related exceptions
                  if (Directory.Exists(path))
                    {
                        //further process the directoy is nested directory is present
                        ProcessDirectory(path, files);
                    }
                    else
                    {
                        //add the file to file list if a nested file is present
                        files.Add(path);
                    }

            return files;
        }
        public static void  ProcessDirectory(string targetDirectory, List<string> files)
        {
           
                // get all the files with .csv extensions
                string[] fileEntries = Directory.GetFiles(targetDirectory, "*.csv");


                // add each to the list of files
                foreach (string entry in fileEntries)
                {
                    files.Add(entry);
                }


                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    ProcessDirectory(subdirectory, files);

                }
            

        }
    }
}

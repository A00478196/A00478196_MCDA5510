using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string path = "D:\\MCDA\\5510\\A00478196_MCDA5510\\ProgAssign1\\ProgAssign1\\Sample Data\\";


            // try catch block to catch any file/directory related exceptions
            try
            {
                

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
              

            }
            catch (FileNotFoundException e)
            {

                Log.Information("A File exception occurred: " + e.Message);

            }
            catch(IndexOutOfRangeException e)
            {
                Log.Information("A out of range exception occured: " + e.Message);
            }

            return files;
        }
        public static void  ProcessDirectory(string targetDirectory, List<string> files)
        {
            try
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
            }catch(DirectoryNotFoundException e)
            {
                Log.Information("A Directory exception occurred: " + e.Message);

            }
            catch (DriveNotFoundException e)
            {
                Log.Information("A Drive exception occurred: " + e.Message);

            }
            catch (IOException e)
            {
                Log.Information("An IOE occurred: " + e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Information("Unauthorized exception occurred: " + e.Message);
            }
          
            
        }
    }
}

using CsvHelper;
using Serilog;
using System.Diagnostics;
using System.Security;

namespace ProgAssign1
{
    public class Home
     
    {

        public static void Main (String[] args)
        {

            try
            {



                // Initial configuration to write log to a log.txt file

                Log.Logger = new LoggerConfiguration()
            .WriteTo.File("..\\..\\..\\logs\\log.txt") // Specify the file path here
            .CreateLogger();


                Log.Information("---- The Program started ----");

                // Start the timer when the program is run
                var timer = new Stopwatch();
                timer.Start();


                List<string> fileEntries = new List<string>();


                // Initialize the classes to read thru the directories and handle the csv files
                DirWalker dirWalk = new DirWalker();
                CSVHandler csvHandler = new CSVHandler();


                //store the read directories in a list called fileEntries
                fileEntries = dirWalk.Walker();

                //pass the list of files to csvhandler class to write to the output along with the timer
                csvHandler.readWrite(fileEntries, timer);



                Log.Information("---- The program has ended... ----");
                Log.Information("");
                Log.CloseAndFlush();
            }
            catch (IOException e)
            {
                Log.Information("An IOE occurred: " + e.Message);
            }
            catch (CsvHelperException e)
            {
                Log.Information("A CsvHelperException occurred: " + e.Message);

            }
            catch (IndexOutOfRangeException e)
            {
                Log.Information("A out of range exception occured: " + e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Log.Information("Unauthorized exception occurred: " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Log.Information("ArgumentNullException occurred: " + e.Message);
            }
            catch (SecurityException e)
            {
                Log.Information("ArgumentNullException occurred: " + e.Message);
            }

          

        }
    }
}